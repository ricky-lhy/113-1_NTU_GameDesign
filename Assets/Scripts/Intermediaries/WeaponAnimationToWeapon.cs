using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weapon;
    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }
    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }
    private void AnimationStartMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }
    private void AnimationStopMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }
    private void AnimationTurnOnFlipTrigger()
    {
        weapon.AnimationTurnOnFlipTrigger();
    }
    private void AnimationTurnOffFlipTrigger()
    {
        weapon.AnimationTurnOffFlipTrigger();
    }
    private void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }
    private void AnimationRangedFinishTrigger()
    {
        weapon.AnimationRangedFinishTrigger();
    }
    private void AnimationRangedStartMovementTrigger()
    {
        weapon.AnimationRangedStartMovementTrigger();
    }
    private void AnimationRangedStopMovementTrigger()
    {
        weapon.AnimationRangedStopMovementTrigger();
    }
    private void AnimationRangedTurnOnFlipTrigger()
    {
        weapon.AnimationRangedTurnOnFlipTrigger();
    }
    private void AnimationRangedTurnOffFlipTrigger()
    {
        weapon.AnimationRangedTurnOffFlipTrigger();
    }
}
