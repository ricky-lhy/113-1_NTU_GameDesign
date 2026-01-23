using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    private Combat Combat {get => combat ??= core.GetCoreComponent<Combat>();}
    private Combat combat;
    public Entity entity;
    public event Action OnHealthZero;
    [SerializeField] public float maxHealth;
    public float currentHealth;
    [SerializeField] public float maxMana;
    public float currentMana;
    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
        currentMana = maxMana;
        entity = transform.parent.parent.GetComponent<Entity>();
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthZero?.Invoke();
            Debug.Log("Dead!");
        }
        else if (transform.gameObject.layer == LayerMask.NameToLayer("Damageable"))
        {
            entity.lastDamageTime = Time.time;
            entity.currentStunResistance --;
            if (entity.currentStunResistance <= 0)
            {
                entity.isStunned = true;
            }
        }
    }
    public void DecreaseHealthWithoutStun(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthZero?.Invoke();
            Debug.Log("Dead!");
        }
        else if (transform.gameObject.layer == LayerMask.NameToLayer("Damageable"))
        {
            entity.lastDamageTime = Time.time;
        }
    }
    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void DecreaseMana(float amount)
    {
        currentMana -= amount;
        if (currentMana <= 0)
        {
            currentMana = 0;
            Debug.Log("No Mana");
        }
    }
    public void IncreaseMana(float amount)
    {
        currentMana += amount;
        if (currentMana >= maxMana)
            currentMana = maxMana;
    }
}
