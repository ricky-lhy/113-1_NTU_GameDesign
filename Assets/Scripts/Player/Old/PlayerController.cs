using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //References for gameobject components
    private Rigidbody2D body;
    private Collider2D myCollider;
    private Animator anim;
    public PlayerStats PS;
    //Parameters for player movements
    //Walking
    public float InputHorizontal, InputVertical;
    private int facingDirection = 1;
    public bool walking, Grounded;
    public float movementSpeed = 10f, movementForceInAir;
    private bool canFlip = true, knockback;
    private float knockbackStartTime;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Vector2 knockbackSpeed;
    //Jumping
    private bool canNormalJump;
    public float jumpforce = 16f;
    public int maxNumjump = 1;
    public int remainingjump;
    private bool isTouchingWall;
    public float airDragMultiplier = 0.9f;
    public float variableJumpHeightMultiplier = 0.5f;
    //Check Surroundings
    public Transform groundcheck;
    public float groundcheckRadius;
    public LayerMask groundMask;
    public Transform wallcheck;
    public float wallCheckDist;
    //Ladder climbing
    public LayerMask ladderMask;
    public bool canClimb;
    public bool isBodyInLadder, isFootInLadder, Climbing;
    private float originalMovementSpeed;
    private float climbSpeed = 3f;
    public Transform footLadderCheck;
    public Collider2D footLadderCheckCollider;
    public Collider2D ladderCollider;
    public Vector3 ladderMidpoint;
    private float originalGravity;
    void Start()
    {
        PS = GetComponent<PlayerStats>();
        body = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        footLadderCheckCollider = footLadderCheck.GetComponent<Collider2D>();
        anim = GetComponent<Animator>(); 
        remainingjump = maxNumjump;
        originalMovementSpeed = movementSpeed;
        originalGravity = body.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove() == false)
            return;
        CheckInput();
        CheckMoveDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckKnockBack();
        CheckSurroundings();
        isBodyInLadder = CheckInsideLadder(myCollider);
        isFootInLadder = CheckInsideLadder(footLadderCheckCollider);
    }
    
    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        if (Grounded && !Climbing)
        {
            movementSpeed = originalMovementSpeed;
            body.gravityScale = originalGravity;
            body.velocity = new Vector2(movementSpeed * InputHorizontal, body.velocity.y);
            if (isBodyInLadder && InputVertical != 0)
            {
                Climbing = true;
                transform.position = new Vector3(ladderMidpoint.x, transform.position.y, transform.position.z);
            }
        }
        else if (Climbing)
        {
            if (isBodyInLadder)
            {
                movementSpeed = 0f;
            }
            else
            {
                movementSpeed = originalMovementSpeed; 
            }
            body.velocity = new Vector2(InputHorizontal * movementSpeed, InputVertical * climbSpeed);
            body.gravityScale = 0f;
            if (!isFootInLadder)
            {
                Climbing = false;
                body.gravityScale = originalGravity;
            }
        }
        else
        {
            if(InputHorizontal != 0)
            {
                Vector2 forceToAdd = new Vector2(movementForceInAir * InputHorizontal, 0);
                body.AddForce(forceToAdd);

                if(Mathf.Abs(body.velocity.x) > movementSpeed)
                {
                    body.velocity = new Vector2(movementSpeed * InputHorizontal, body.velocity.y);
                }
            }
            else
            {
                body.velocity = new Vector2(body.velocity.x * airDragMultiplier, body.velocity.y);
            }
        }
    }
    private bool CheckInsideLadder(Collider2D objCollider)
    {
        // Get the bounds of the object you are checking
        Bounds myBounds = objCollider.bounds;

        Vector3 midpoint = myBounds.center;
        // Use OverlapPointAll to check if the midpoint is inside any collider on the target layer
        Collider2D[] colliders = Physics2D.OverlapPointAll(midpoint);

        // Check if any of the colliders belong to the target layer
        foreach (var collider in colliders)
        {
            if (collider != null && (ladderMask == (ladderMask | (1 << collider.gameObject.layer))))
            {
                ladderMidpoint = collider.bounds.center;
                return true;  // Midpoint is inside a collider on the target layer
            }
        }

        // Midpoint is not inside any collider on the target layer
        return false;
    }

    private bool CanMove() {
        bool canMove = true;
        if (FindObjectOfType<InteractionSystem>().isExamine)
            canMove = false;
        if (FindObjectOfType<InventorySystem>().isOpen)
            canMove = false;
        if (PS.isDead)
            canMove = false;
        return canMove;
    }
    private void CheckInput()
    {
        //GetAxisRaw: Pressing A return -1, Pressing D return = 1
        InputHorizontal = Input.GetAxisRaw("Horizontal");
        InputVertical = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
            NormalJump();

        if (Input.GetButtonUp("Jump"))
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * variableJumpHeightMultiplier);
    }

    private void CheckMoveDirection()
    {
        if (InputHorizontal < 0 && facingDirection > 0)
        {
            Flip();
        }
        else if (InputHorizontal > 0 && facingDirection < 0)
        {
            Flip();
        }

        if (Mathf.Abs(body.velocity.x) >= 0.01f)
            walking = true;
        else
            walking = false;
    }

    public void DisableFlip()
    {
        canFlip = false;
    }
    public void EnableFlip()
    {
        canFlip = true;
    }
    private void Flip()
    {
        if (!knockback && canFlip)
        {
            facingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    private void CheckSurroundings()
    {
        Grounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckRadius, groundMask);
        isTouchingWall = Physics2D.Raycast(wallcheck.position, transform.right, wallCheckDist, groundMask);
    }
    private void CheckIfCanJump()
    {
        //Allow more than one jump if we set maxNumjump > 1
        if (Grounded && body.velocity.y <= 0.01f)
            remainingjump = maxNumjump;
        
        if (remainingjump <= 0)
            canNormalJump = false;
        else
            canNormalJump = true;
    }
    // private void ApplyMovement()
    // {
    //     if (Grounded)
    //         body.velocity = new Vector2(movementSpeed * InputHorizontal, body.velocity.y);
        
    //     else if(!Grounded && InputHorizontal != 0)
    //     {
    //         Vector2 forceToAdd = new Vector2(movementForceInAir * InputHorizontal, 0);
    //         body.AddForce(forceToAdd);

    //         if(Mathf.Abs(body.velocity.x) > movementSpeed)
    //         {
    //             body.velocity = new Vector2(movementSpeed * InputHorizontal, body.velocity.y);
    //         }
    //     }
    //     else if(!Grounded && InputHorizontal == 0)
    //     {
    //         body.velocity = new Vector2(body.velocity.x * airDragMultiplier, body.velocity.y);
    //     }
    // }

    public int GetFacingDirection()
    {
        return facingDirection;
    }
    private void NormalJump()
    {
        if (canNormalJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpforce);
            remainingjump --;
        }
    }
    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        body.velocity = new Vector2(knockbackSpeed.x *direction, knockbackSpeed.y);
    }
    private void CheckKnockBack()
    {
        if (knockback && Time.time >= knockbackStartTime + knockbackDuration)
        {
            knockback = false;
            body.velocity = new Vector2(0f, body.velocity.y);
        }
    }
    private void UpdateAnimations()
    {
        //Set value in the animator to start/stop animation
        anim.SetBool("isWalking", walking);
        anim.SetBool("isGrounded", Grounded);
        anim.SetFloat("yVelocity", body.velocity.y);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundcheck.position, groundcheckRadius);
        Gizmos.DrawLine(wallcheck.position, new Vector3(wallcheck.position.x + wallCheckDist, wallcheck.position.y, wallcheck.position.z));
    }

    

}
