using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public Action onDeath;

    public float MaxHealth;
    public float health;

    public bool isDead;


    private void OnEnable() {
        health = MaxHealth;
        isDead = false;
    }
    
    
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        health -= damage;
        if (health <= 0)
        {
            DeathBehavior();
        }
    }

    void DeathBehavior()
    {
        onDeath?.Invoke();
        //gameObject.SetActive(false);
        isDead = true;
    }
}
