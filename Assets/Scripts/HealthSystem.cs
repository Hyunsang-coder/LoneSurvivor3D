using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float MaxHealth;
    public float health;


    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        return;
    }
}
