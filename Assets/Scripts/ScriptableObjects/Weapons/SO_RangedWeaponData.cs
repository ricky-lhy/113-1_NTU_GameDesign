using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedWeaponData", menuName = "Data/Weapon Data/Ranged Weapon")]
public class SO_RangedWeaponData : SO_WeaponData
{
    [SerializeField] public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileTravelDist;
    public float projectileDamage;
    public float consumeMana;
    [SerializeField] private WeaponAttackDetails[] attackDetails;
    public WeaponAttackDetails[] AttackDetails {get => attackDetails; private set => attackDetails = value;}
    private void OnEnable() 
    {
        amountOfAttacks = attackDetails.Length;
        movementSpeed = new float[amountOfAttacks];
        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }   
    }
}
