// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// public class PlayerCombatController : MonoBehaviour
// {
//     [SerializeField] private bool combatEnabled = true;
//     [SerializeField] private float inputTimer, attack1_Radius, attack1_Damage;
//     [SerializeField] private float stunDamageAmount = 1f;
//     [SerializeField] private Transform attack1_HitBoxPos;
//     [SerializeField] private LayerMask Damageable;
    
//     private bool gotInput, isAttacking, isFirstAttack;
//     private float lastInputTime = Mathf.NegativeInfinity;
//     private AttackDetails attackDetails;
//     private Animator anim;
//     private PlayerController pc;
//     private PlayerStats ps;

//     private void Start() 
//     {
//         pc = GetComponent<PlayerController>();
//         ps = GetComponent<PlayerStats>();
//         anim = GetComponent<Animator>();
//         // anim.SetBool("canAttack", combatEnabled);
//     }

//     private void Update()
//     {
//         CheckCombatInput();
//         CheckAttacks();
//     }

//     private void CheckCombatInput()
//     {
//         if (Input.GetMouseButtonDown(0)) //Mouse: LeftClick
//         {
//             if (combatEnabled)
//             {
//                 //Attempt combat
//                 gotInput = true;
//                 lastInputTime = Time.time; //Record last time we press attack
//             }
//         }
//     }

//     private void CheckAttacks()
//     {
//         if (gotInput)
//         {
//             //Perform attack 1
//             if (!isAttacking)
//             {
//                 gotInput = false;
//                 isAttacking = true;
//                 isFirstAttack = !isFirstAttack;
//                 anim.SetBool("attack1", true);
//                 anim.SetBool("firstAttack", isFirstAttack);
//                 anim.SetBool("isAttacking", isAttacking);
//             }
//         }

//         if (Time.time >= lastInputTime + inputTimer)
//         {
//             //Wait for new input
//             gotInput = false;
//         }
//     }

//     private void CheckAttackHitBox()
//     {
//         Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1_HitBoxPos.position, attack1_Radius, Damageable);
//         attackDetails.damageAmount = attack1_Damage;
//         attackDetails.position = transform.position;
//         attackDetails.stunDamageAmount = stunDamageAmount;
//         foreach(Collider2D collider in detectedObjects)
//         {
//             //Call specific function on a script on an object without knowing which script it is
//             collider.transform.parent.SendMessage("Damage", attackDetails, SendMessageOptions.DontRequireReceiver);
//             //Instantiate hit particle
//         }
//     }

//     private void FinishAttack1()
//     {
//         isAttacking = false;
//         anim.SetBool("isAttacking", isAttacking);
//         anim.SetBool("attack1", false);
//     }
//     public void Damage(AttackDetails attackDetails)
//     {
//             int direction;
//             ps.DecreaseHealth(attackDetails.damageAmount);
//             if (attackDetails.position.x < transform.position.x)
//                 direction = 1;
//             else
//                 direction = -1;
//             pc.Knockback(direction);  
//     }

//     private void OnDrawGizmos()
//     {
//         Gizmos.DrawWireSphere(attack1_HitBoxPos.position, attack1_Radius);
//     }
// }
