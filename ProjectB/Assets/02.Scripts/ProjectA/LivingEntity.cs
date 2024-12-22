using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 50;
    protected float health;
    protected bool dead;

    protected virtual void Start()
    {
        health = startingHealth;
        Debug.Log(health);
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if (health <=0)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        Debug.Log(health + " Á×À½ ");
        GameObject.Destroy(gameObject);
    }
}
