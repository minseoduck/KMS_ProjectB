using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Controller_B : MonoBehaviour
{
   
    public Transform weaponHold;
    public Gun_B startingGun;

    private Gun_B equippedGun;

    private void Start()
    {
      
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }
    }

    public void EquipGun(Gun_B gunToEquip)
    {
        if (gunToEquip == null)
        {
            Debug.LogWarning("¿Â¬¯X");
            return;
        }

      
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }

       
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation);
        equippedGun.transform.SetParent(weaponHold, false); 
        Debug.Log("√—¿Â¬¯");
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.TryShoot();
        }
        else
        {
            Debug.LogWarning("πﬂªÁX");
        }
    }
}

