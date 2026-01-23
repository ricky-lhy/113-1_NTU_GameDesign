using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{
    private Movement Movement {get => movement ??= core.GetCoreComponent<Movement>();}
    private Movement movement;
    protected SO_AggressiveWeaponData aggressiveWeaponData;
    private List<IDamageable> detectedDamageable = new List<IDamageable>();
    private List<IKnockbackable> detectedKnockbackable = new List<IKnockbackable>();
    private AudioSource weaponAudio;
    public AudioClip slashSound;
    public AudioClip fleshSound;
    protected override void Awake()
    {
        base.Awake();
        if (weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
            weaponAudio = GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Wrong data for the weapon");
        }
    }
    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        // weaponAudio.clip = slashSound;
        // weaponAudio.loop = false;
        weaponAudio.PlayOneShot(slashSound);
        CheckMeleeAttack();
    }
    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];
        foreach (IDamageable item in detectedDamageable.ToList())
        {
            item.Damage(details.damageAmount);
            // weaponAudio.clip = fleshSound;
            // weaponAudio.loop = false;
            weaponAudio.PlayOneShot(fleshSound);
        }
        foreach (IKnockbackable item in detectedKnockbackable.ToList())
        {
            item.Knockback(details.knockbackStrength, details.knockbackAngle, Movement.FacingDirection);
        }
    }
    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        if (knockbackable != null)
        {
            detectedKnockbackable.Add(knockbackable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }
        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        if (knockbackable != null)
        {
            detectedKnockbackable.Remove(knockbackable);
        }
    }
}
