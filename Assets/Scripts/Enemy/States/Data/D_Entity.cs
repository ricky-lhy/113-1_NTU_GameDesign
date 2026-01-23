using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30f;
    public float groundCheckRadius = 0.3f;
    public float wallCheckDist = 0.2f;
    public float ledgeCheckDist = 1f;
    public float minAgroDist = 3f;
    public float maxAgroDist = 4f;
    public float closeRangeActionDist = 1f;
    public float damageHopSpeed = 12f;
    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;
    public GameObject hitParticle;
    public LayerMask groundMask;
    public LayerMask playerMask;
}
