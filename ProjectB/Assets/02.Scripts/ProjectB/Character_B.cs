using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_B : MonoBehaviour
{
    [SerializeField]
    private HealthData_B healthData; 

    public void TakeDamage(float amount)
    {
        healthData.currentHealth -= amount; 
        Debug.Log($"{gameObject.name} ü��: {healthData.currentHealth}");

        if (healthData.currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ���!");
        Destroy(gameObject, 2f);
    }
}
