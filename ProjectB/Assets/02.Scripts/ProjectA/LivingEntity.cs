using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 50;
    protected float health;
    protected bool dead;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
        Debug.Log(health);
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        Debug.Log(health + " Á×À½ ");
        GameObject.Destroy(gameObject);
    }
}
