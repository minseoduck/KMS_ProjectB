using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_B : MonoBehaviour
{
    [SerializeField]
    private BulletData bulletData; // ScriptableObject 참조

    public float speed = 20f;
    public float lifeTime = 3f;

    private Bullet_Pool_B pool;

    public void Initialize(Vector3 velocity, Bullet_Pool_B objectPool)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = velocity;
        }

        pool = objectPool;

        // 이전에 Invoke된 ReturnToPool 취소
        CancelInvoke(nameof(ReturnToPool));
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    private void ReturnToPool()
    {
        pool.ReturnObject(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Character_B character = collision.gameObject.GetComponent<Character_B>();
        if (character != null)
        {
            character.TakeDamage(bulletData.damage);
        }

        pool.ReturnObject(gameObject);
    }
}
