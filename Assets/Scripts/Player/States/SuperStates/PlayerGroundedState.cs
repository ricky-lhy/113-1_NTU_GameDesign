using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    private bool jumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    protected bool isTouchingCeiling;
    private bool dashInput;
    protected Movement Movement {
        get => movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement movement;
    private CollisionSenses CollisionSenses {
        get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>();
    }
    private CollisionSenses collisionSenses;
    protected Stats Stats {
        get => stats ??= core.GetCoreComponent<Stats>();
    }
    private Stats stats;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {}
    public override void DoChecks()
    {
        base.DoChecks();
        if (CollisionSenses) {
            isGrounded = CollisionSenses.Grounded;
            isTouchingWall = CollisionSenses.WallFront;;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            isTouchingCeiling = CollisionSenses.Ceiling;
        } 
    }
    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling && Stats.currentMana >= playerData.arrowMana && Time.time >= player.lastShootingTime + player.shootCooldown)
        {
            stateMachine.ChangeState(player.SecondaryRangedAttackState);
        }
        else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
