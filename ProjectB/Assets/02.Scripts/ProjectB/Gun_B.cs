using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_B : MonoBehaviour
{
    [SerializeField] private Transform muzzle; // 총구 위치
    [SerializeField] private float timeBetweenShots = 1.0f; // 발사 간격 (초 단위)
    [SerializeField] private float bulletSpeed = 5f; // 탄환 속도
   public Bullet_Pool_B bulletPool; // 오브젝트 풀 참조

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
