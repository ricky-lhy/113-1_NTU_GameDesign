using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Stats playerStats;
    private Image HPIndicator;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponentInChildren<Stats>();
        HPIndicator = GameObject.Find("HealthBarFill").GetComponent<Image>();
        HPIndicator.fillAmount = 1f;
        HPIndicator.color = Color.green;
    }

    void Update()
    {
        if (playerStats.currentHealth <= 0f)
            HPIndicator.fillAmount = 0f;
        else
            HPIndicator.fillAmount = playerStats.currentHealth/playerStats.maxHealth;
        if (playerStats.currentHealth/playerStats.maxHealth > 0.5f)
                HPIndicator.color = Color.green;
        else if (playerStats.currentHealth/playerStats.maxHealth > 0.2f && playerStats.currentHealth/playerStats.maxHealth <= 0.5f)
            HPIndicator.color = Color.yellow;
        else if (playerStats.currentHealth/playerStats.maxHealth < 0.2f)
            HPIndicator.color = Color.red;
    }
}
