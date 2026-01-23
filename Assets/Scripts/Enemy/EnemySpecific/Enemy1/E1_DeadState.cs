using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DeadState : DeadState
{
    private Enemy1 enemy;
    private List<GameObject> dropItems = new List<GameObject>();
    public E1_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        dropItems.Add(GameObject.Find("Coin"));
        dropItems.Add(GameObject.Find("Apple"));
        dropItems.Add(GameObject.Find("SmallPotion"));
        base.Enter();
        GameObject dropSource = dropItems[Random.Range(0, dropItems.Count)];
        GameObject dropClone = GameObject.Instantiate(dropSource, enemy.transform.position, Quaternion.Euler(0, 0, 0));
        dropClone.GetComponent<Collider2D>().isTrigger = false;
        dropClone.GetComponent<Rigidbody2D>().gravityScale = 1f;
        dropClone.name = dropSource.name;
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
