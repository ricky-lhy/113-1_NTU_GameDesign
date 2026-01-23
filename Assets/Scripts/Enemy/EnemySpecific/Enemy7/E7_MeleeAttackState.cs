using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_MeleeAttackState : MeleeAttackState
{
    private Enemy7 enemy;

    public E7_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Enemy7 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
