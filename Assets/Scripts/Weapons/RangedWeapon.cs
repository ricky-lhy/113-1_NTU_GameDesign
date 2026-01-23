using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RangedWeapon : Weapon
{
    protected SO_RangedWeaponData rangedWeaponData;
    private Movement Movement {get => movement ??= core.GetCoreComponent<Movement>();}
    private Movement movement;
    private GameObject projectile;
    private PlayerProjectile projectileScript;
    public bool canShoot;
    private AudioSource arrowSound;
    public AudioClip arrowClip;
    protected override void Awake()
    {
        base.Awake();
        if (weaponData.GetType() == typeof(SO_RangedWeaponData))
        {
            rangedWeaponData = (SO_RangedWeaponData)weaponData;
            arrowSound = GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Wrong data for the weapon");
        }
    }
    public override void EnterWeapon()
    {
        base.EnterWeapon();
    }
    public override void ExitWeapon()
    {
        base.ExitWeapon();
    }
    public override void AnimationActionTrigger()
    {
        // if (Time.time >= lastShootingTime + shootCooldown)
        // {
            base.AnimationActionTrigger();
            projectile = GameObject.Instantiate(rangedWeaponData.projectilePrefab, transform.position, transform.rotation);
            projectileScript = projectile.GetComponent<PlayerProjectile>();
            projectileScript.FireProjectile(rangedWeaponData.projectileSpeed, rangedWeaponData.projectileTravelDist, rangedWeaponData.projectileDamage);
            Stats.DecreaseMana(rangedWeaponData.consumeMana); 
            arrowSound.PlayOneShot(arrowClip);
        // }
    }
}
