using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private Movement Movement {get => movement ??= Core.GetCoreComponent<Movement>();}
    private Movement movement;
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public Core Core {get; private set;}
    public Animator anim {get; private set;}
    public AnimationToStateMachine atsm {get; private set;}
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;
    private Vector2 velocityWorkspace;
    public float lastDamageTime;
    public int lastDamageDirection {get; private set;}
    private float currentHealth;
    public float currentStunResistance;
    public bool isStunned;
    protected bool isDead;
    public virtual void Awake() 
    {
        Core = GetComponentInChildren<Core>();
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();
        stateMachine = new FiniteStateMachine();
    }
    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
        anim.SetFloat("yVelocity", Movement.RB.velocity.y);
        // if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        // {
        //     ResetStunResistance();
        // }
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDist, entityData.playerMask);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDist, entityData.playerMask);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDist, entityData.playerMask);
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }
    // public virtual void Damage(AttackDetails attackDetails)
    // {
    //     lastDamageTime = Time.time;
    //     currentHealth -= attackDetails.damageAmount;
    //     currentStunResistance -= attackDetails.stunDamageAmount;
    //     DamageHop(entityData.damageHopSpeed);
    //     Instantiate(entityData.hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    //     if (attackDetails.position.x > transform.position.x)
    //     {
    //         lastDamageDirection = -1;
    //     }
    //     else
    //     {
    //         lastDamageDirection = 1;
    //     }
    //     if (currentStunResistance <= 0)
    //     {
    //         isStunned = true;
    //     }
    //     if (currentHealth <= 0)
    //     {
    //         isDead = true;
    //     }
    // }
    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.RB.velocity.x, velocity);
        Movement.RB.velocity = velocityWorkspace;
    }
    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.wallCheckDist));    
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDist));    
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDist * Movement.FacingDirection), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDist * Movement.FacingDirection), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDist * Movement.FacingDirection), 0.2f);
        }
    }
}
