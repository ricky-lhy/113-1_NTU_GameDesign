using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine {get; private set;}
    public PlayerIdleState IdleState {get; private set;}
    public PlayerMoveState MoveState {get; private set;}
    public PlayerJumpState JumpState {get; private set;}
    public PlayerInAirState InAirState {get; private set;}
    public PlayerLandState LandState {get; private set;}
    public PlayerWallSlideState WallSlideState {get; private set;}
    public PlayerWallGrabState WallGrabState {get; private set;}
    public PlayerWallClimbState WallClimbState {get; private set;}
    public PlayerWallJumpState WallJumpState {get; private set;}
    public PlayerLedgeClimbState LedgeClimbState {get; private set;}
    public PlayerDashState DashState {get; private set;}
    public PlayerCrouchIdleState CrouchIdleState {get; private set;}
    public PlayerCrouchMoveState CrouchMoveState {get; private set;}
    public PlayerAttackState PrimaryAttackState {get; private set;}
    public PlayerAttackState SecondaryAttackState {get; private set;}
    public PlayerRangedAttackState SecondaryRangedAttackState {get; private set;}
    #endregion
    
    #region Components
    public Core Core {get; private set;}
    public Animator Anim {get; private set;}
    public PlayerInputHandler InputHandler {get; private set;}
    public Rigidbody2D RB {get; private set;}
    public BoxCollider2D MovementCollider {get; private set;}
    public Transform DashDirectionIndicator {get; private set;}
    public WeaponInventory Inventory {get; private set;}
    public AudioSource playerAudioSource {get; private set;}
    #endregion
    

    #region Other Variables
    [SerializeField] private PlayerData playerData;
    private Vector2 workspace;
    public bool canDash;
    public float dashCooldown;
    public float lastShootingTime;
    public float shootCooldown = 2f;
    public AudioClip movingSound;
    public AudioClip dashSound;
    #endregion

    #region Unity Callback Function
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        StateMachine = new PlayerStateMachine();    
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryRangedAttackState = new PlayerRangedAttackState(this, StateMachine, playerData, "rangedAttack");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Inventory = GetComponent<WeaponInventory>();
        playerAudioSource = GetComponent<AudioSource>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        SecondaryRangedAttackState.SetRangedWeapon(Inventory.weapons[(int)CombatInputs.secondary]);
        StateMachine.Initialize(IdleState);
        canDash = DashState.CanDash;
        dashCooldown = playerData.dashCooldown;
        lastShootingTime = -Mathf.Infinity;
    }
    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();    
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();    
    }
    #endregion
    


    

    #region Other Functions
    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);
        center.y += (height - MovementCollider.size.y) / 2;
        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }
    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger () => StateMachine.CurrentState.AnimationFinishTrigger();
    public void PlayWalkingSound() {
        // playerAudioSource.clip = movingSound;
        // playerAudioSource.loop = true;
        playerAudioSource.PlayOneShot(movingSound);
    }
    public void PlayDashSound() {
        playerAudioSource.PlayOneShot(dashSound);
    }
    public void StopPlayingSound()
    {
        playerAudioSource.loop = false;
        playerAudioSource.Stop();
    }
    #endregion
}
