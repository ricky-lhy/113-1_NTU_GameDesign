using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] private GameObject deathChunkParticle, deathBloodParticle;
    public float currentHealth;
    public bool isDead = false;
    // private LevelManager LM;
    public Transform healthBarBG;
    public Image fillBar;
    PlayerController PC;
    public void Start()
    {
        currentHealth = maxHealth;
        // LM = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        healthBarBG = GameObject.Find("HealthBar").transform.GetChild(0);
        fillBar = healthBarBG.GetChild(0).GetComponent<Image>();
        fillBar.fillAmount = 1f;
        fillBar.color = Color.green;
        PC = GetComponent<PlayerController>();
    }

    void Update()
    {
        UpdateHealthBar();
        
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();
        if (currentHealth <= 0f)
        {
            Die();    
        }
    }
    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        if (currentHealth <= 0f)
            fillBar.fillAmount = 0f;
        else
            fillBar.fillAmount = currentHealth/maxHealth;
        if (currentHealth/maxHealth > 0.5f)
                fillBar.color = Color.green;
        else if (currentHealth/maxHealth > 0.2f && currentHealth/maxHealth <= 0.5f)
            fillBar.color = Color.yellow;
        else if (currentHealth/maxHealth < 0.2f)
            fillBar.color = Color.red;
    }
    private void Die()
    {
        isDead = true;
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        // LM.Respawn();
        // Destroy(gameObject);
    }
    public void ResetPlayer()
    {
        PC.InputHorizontal = 0;
        PC.InputVertical = 0;
        currentHealth = maxHealth;
        fillBar.fillAmount = 1f;
        fillBar.color = Color.green;
        isDead = false;
        PC.enabled = true;
    }
}
