using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B4_MoveState : MoveState
{
    private Boss4 enemy;
    public B4_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Boss4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }    
}
