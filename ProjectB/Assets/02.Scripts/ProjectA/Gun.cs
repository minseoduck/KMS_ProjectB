using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Bullet bullet;
    //연사력(격발 사이의 간격 ) 
    public float msBetweenShots = 100;
    //발사 순간 총알 속력 
    public float muzzleVelocity = 35;

    float nextShotTime;

    public void Shoot()
    {
        if (Time.time >nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Bullet newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation) as Bullet;
            newBullet.SetSpeed(muzzleVelocity);
        }
    
    }

}
