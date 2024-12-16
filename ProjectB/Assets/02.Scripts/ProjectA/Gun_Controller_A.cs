using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Controller_A : MonoBehaviour
{
    public Transform weapoinHold;
    public Gun startigGun;
    Gun equippedGun;

    private void Start()
    {
        if (startigGun!=null)
        {
            EquipGun(startigGun);
        }
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun !=null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, weapoinHold.position, weapoinHold.rotation) as Gun;
        equippedGun.transform.parent = weapoinHold;
        Debug.Log("ÃÑÀåÂø");
    }
    public void Shoot()
    {
        if (equippedGun !=null)
        {
            equippedGun.Shoot();
        }
    }
}
