using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private int xInput;
    private float velcoityToSet;
    private bool setVelocity;
    private bool shouldCheckFlip;
    public PlayerRangedAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        setVelocity = false;
        weapon.EnterWeapon();
        player.lastShootingTime = Time.time;
    }
    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        if (shouldCheckFlip)
        {
            Movement?.CheckIfShouldFlip(xInput);
        }
        if (setVelocity)
        {
            Movement?.SetVelocityX(velcoityToSet * Movement.FacingDirection);
        }
    }
    public void SetRangedWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializateRangedWeapon(this, core);
    }
    public void SetPlayerVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * Movement.FacingDirection);
        velcoityToSet = velocity;
        setVelocity = true;
    }
    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }
    #region Animation Triggers
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
    #endregion
}
