using UnityEngine;

public class Character_B : MonoBehaviour
{
    [SerializeField]
    protected HealthData_B healthData; // ü�� ������
    public event System.Action OnDeath; // ��� �̺�Ʈ

    public virtual void TakeDamage(float amount)
    {
        healthData.currentHealth -= amount;
        Debug.Log($"{gameObject.name} ü��: {healthData.currentHealth}");

        if (healthData.currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        InvokeOnDeath();  // ��� �̺�Ʈ ȣ��
        Debug.Log($"{gameObject.name} ���!");
        Destroy(gameObject, 2f); // 2�� �� ������Ʈ ����
    }
    protected void InvokeOnDeath()
    {
        OnDeath?.Invoke(); // �̺�Ʈ�� �����ϰ� ȣ��
    }
}
