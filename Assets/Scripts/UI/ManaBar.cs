using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{
    private Stats playerStats;
    private Image ManaIndicator;
    private Color ManaBarColor = new Color (0f, 0.5f, 1f);

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponentInChildren<Stats>();
        ManaIndicator = GameObject.Find("ManaBarFill").GetComponent<Image>();
        ManaIndicator.fillAmount = 1f;
        ManaIndicator.color = ManaBarColor;
    }

    void Update()
    {
        if (playerStats.currentHealth <= 0f)
            ManaIndicator.fillAmount = 0f;
        else
            ManaIndicator.fillAmount = playerStats.currentMana/playerStats.maxMana;
    }
}
