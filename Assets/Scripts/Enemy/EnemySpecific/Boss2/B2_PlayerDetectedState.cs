using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_PlayerDetectedState : PlayerDetectedState
{
    private Boss2 enemy;
    public B2_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Boss2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction && Time.unscaledTime >= stateData.attackCooldown + enemy.lastAttackFinishedTime) 
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            Movement.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }
    
}
