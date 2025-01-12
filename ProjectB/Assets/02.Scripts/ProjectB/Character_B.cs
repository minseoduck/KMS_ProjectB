using UnityEngine;

public class Character_B : MonoBehaviour
{
    [SerializeField]
    protected HealthData_B healthData; // 체력 데이터
    public event System.Action OnDeath; // 사망 이벤트

    public virtual void TakeDamage(float amount)
    {
        healthData.currentHealth -= amount;
        Debug.Log($"{gameObject.name} 체력: {healthData.currentHealth}");

        if (healthData.currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        InvokeOnDeath();  // 사망 이벤트 호출
        Debug.Log($"{gameObject.name} 사망!");
        Destroy(gameObject, 2f); // 2초 후 오브젝트 삭제
    }
    protected void InvokeOnDeath()
    {
        OnDeath?.Invoke(); // 이벤트를 안전하게 호출
    }
}
