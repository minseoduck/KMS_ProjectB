using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_B : MonoBehaviour
{
    [SerializeField] private Transform muzzle; // �ѱ� ��ġ
    [SerializeField] private float timeBetweenShots = 1.0f; // �߻� ���� (�� ����)
    [SerializeField] private float bulletSpeed = 5f; // źȯ �ӵ�
   public Bullet_Pool_B bulletPool; // ������Ʈ Ǯ ����

    private float _nextShotTime;

   

    public void TryShoot()
    {
        if (Time.time >= _nextShotTime)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
       

        _nextShotTime = Time.time + timeBetweenShots;

        GameObject bulletObj = bulletPool.GetObject();
     

        bulletObj.transform.position = muzzle.position;
        bulletObj.transform.rotation = muzzle.rotation;

        Bullet_B bullet = bulletObj.GetComponent<Bullet_B>();
        bullet.Initialize(muzzle.forward * bulletSpeed, bulletPool);
    }
}
