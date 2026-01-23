using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_LookForPlayerState : LookForPlayerState
{
    private Boss1 enemy;
    public B1_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Boss1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
