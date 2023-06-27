using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float MaxHealth;
    public float health;

    public bool isDead;


    private void OnEnable() {
        health = MaxHealth;
        isDead = false;
    }
    
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathBehavior();
        }
    }

    void DeathBehavior()
    {
        gameObject.SetActive(false);
        isDead = true;
        return;
    }
}
