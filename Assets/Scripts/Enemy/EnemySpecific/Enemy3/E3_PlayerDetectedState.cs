using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_PlayerDetectedState : PlayerDetectedState
{
    private Enemy3 enemy;

    public E3_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (performCloseRangeAction)
        {
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown)
            {
                stateMachine.ChangeState(enemy.dodgeState);
            }
            else if (Time.unscaledTime >= stateData.attackCooldown + enemy.lastAttackFinishedTime)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.rangedAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        // Refer to E1, if error, should delete the below part
        else if (!isDetectingLedge)
        {
            Movement.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
