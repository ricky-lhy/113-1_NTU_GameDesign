using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;
    public Animator baseAnimator;
    public Animator weaponAnimator;
    protected PlayerAttackState state;
    protected PlayerRangedAttackState rangedState;
    protected Core core;
    protected Stats Stats {
        get => stats ??= core.GetCoreComponent<Stats>();
    }
    private Stats stats;
    protected int attackCounter;
    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);
        if (attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }
        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);
        baseAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }
    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);
        attackCounter++;
        gameObject.SetActive(false);
    }

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }
    public virtual void AnimationRangedFinishTrigger()
    {
        rangedState.AnimationFinishTrigger();
    }
    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }
    public virtual void AnimationRangedStartMovementTrigger()
    {
        rangedState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }
    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }
    public virtual void AnimationRangedStopMovementTrigger()
    {
        rangedState.SetPlayerVelocity(0f);
    }
    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }
    public virtual void AnimationRangedTurnOffFlipTrigger()
    {
        rangedState.SetFlipCheck(false);
    }
    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);
    }
    public virtual void AnimationRangedTurnOnFlipTrigger()
    {
        rangedState.SetFlipCheck(true);
    }
    public virtual void AnimationActionTrigger()
    {
        
    }
    #endregion
    public void InitializateWeapon(PlayerAttackState state, Core core)
    {
        this.state = state;
        this.core = core;
    }
    public void InitializateRangedWeapon(PlayerRangedAttackState rangedState, Core core)
    {
        this.rangedState = rangedState;
        this.core = core;
    }
}
