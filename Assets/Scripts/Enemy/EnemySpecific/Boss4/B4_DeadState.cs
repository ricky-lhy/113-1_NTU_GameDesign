using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B4_DeadState : DeadState
{
    private Boss4 enemy;
    // private List<GameObject> dropItems = new List<GameObject>();
    public B4_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Boss4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        // dropItems.Add(GameObject.Find("Coin"));
        // dropItems.Add(GameObject.Find("Apple"));
        // dropItems.Add(GameObject.Find("SmallPotion"));
        base.Enter();
        // GameObject dropSource = dropItems[Random.Range(0, dropItems.Count)];
        // GameObject dropClone = GameObject.Instantiate(dropSource, enemy.aliveGO.transform.position, Quaternion.Euler(0, 0, 0));
        // dropClone.name = dropSource.name;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
