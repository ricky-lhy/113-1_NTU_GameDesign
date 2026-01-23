using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;
    [SerializeField] private bool applyKnockback;
    [SerializeField] private GameObject HitParticle;
    private float currentHealth, knockbackStart;
    private int playerFacingDirection;
    private bool playerOnLeft, knockback;
    private PlayerController pc;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D bodyAlive, bodyBrokenTop, bodyBrokenBot;
    private Animator aliveAnim;

    private void Start()
    {
        currentHealth = maxHealth;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("BrokenTop").gameObject;
        brokenBotGO = transform.Find("BrokenBottom").gameObject;
        aliveAnim = aliveGO.GetComponent<Animator>();
        bodyAlive = aliveGO.GetComponent<Rigidbody2D>();
        bodyBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        bodyBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();
        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockBack();
    }
    // private void Damage(AttackDetails attackDetails)
    // {
    //     currentHealth -= attackDetails.damageAmount;
    //     playerFacingDirection = pc.GetFacingDirection();
    //     Instantiate(HitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        
    //     if(playerFacingDirection == 1)
    //         playerOnLeft = true;
    //     else
    //         playerOnLeft = false;
        
    //     aliveAnim.SetBool("PlayerOnLeft", playerOnLeft);
    //     aliveAnim.SetTrigger("Damage");
    //     if (applyKnockback && currentHealth > 0f)
    //         Knockback(); //Knockback effect
        
    //     if (currentHealth <= 0f)
    //     {
    //         Die();
    //     }
    // }

    private void CheckKnockBack()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            bodyAlive.velocity = new Vector2(0f, bodyAlive.velocity.y);
        }
    }
    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        bodyAlive.velocity = new Vector2(knockbackSpeedX*playerFacingDirection, knockbackSpeedY);
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);
        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        bodyBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        bodyBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        bodyBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
