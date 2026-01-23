using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_MeleeAttackState : MeleeAttackState
{
    private Boss2 enemy;

    public B2_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Boss2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            enemy.lastAttackFinishedTime = Time.unscaledTime;
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
