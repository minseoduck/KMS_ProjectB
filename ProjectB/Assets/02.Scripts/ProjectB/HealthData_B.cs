using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/HealthData", order = 1)]
public class HealthData_B : ScriptableObject
{
    public float maxHealthf; // 최대 체력
    public float currentHealth; // 현재 체력
   
}