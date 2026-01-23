using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : Entity
{
    public E7_IdleState idleState {get; private set;}
    public E7_MoveState moveState {get; private set;}
    public E7_PlayerDetectedState playerDetectedState {get; private set;}
    public E7_ChargeState chargeState {get; private set;}
    public E7_LookForPlayerState lookForPlayerState {get; private set;}
    public E7_MeleeAttackState meleeAttackState {get; private set;}
    public E7_StunState stunState {get; private set;}
    public E7_DeadState deathState {get; private set;}
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deathStateData;

    [SerializeField] private Transform meleeAttackPosition;
    public float lastAttackFinishedTime;
    public override void Awake()
    {
        base.Awake();
        moveState = new E7_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E7_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E7_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E7_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E7_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E7_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new E7_StunState(this, stateMachine, "stun", stunStateData, this);
        deathState = new E7_DeadState(this, stateMachine, "dead", deathStateData, this);
    }
    private void Start() {
        stateMachine.Initialize(moveState);
    }
    public override void Update()
    {
        base.Update();
        if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            base.ResetStunResistance();
        }
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
    // public override void Damage(AttackDetails attackDetails)
    // {
    //     base.Damage(attackDetails);
    //     if (isDead)
    //     {
    //         stateMachine.ChangeState(deathState);
    //     }
    //     else if (isStunned && stateMachine.currentState != stunState)
    //     {
    //         stateMachine.ChangeState(stunState);
    //     }
        
    // }
}
