using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/HealthData", order = 1)]
public class HealthData_B : ScriptableObject
{
    public float maxHealth = 100f; // �ִ� ü��
    public float currentHealth = 100f; // ���� ü��
   
}