using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Nightmare
{
    public class AmmoManager : MonoBehaviour
    {
        WeaponSwitching weaponSwitching;
        Text sText;

        void Awake ()
        {
            sText = GetComponent <Text> ();
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            weaponSwitching = player.GetComponentInChildren<WeaponSwitching>();
        }


        void Update ()
        {
            Gun currentWeaponGun = weaponSwitching.GetCurrentWeapon().GetComponent<Gun>();
            Melee currentWeaponMelee = weaponSwitching.GetCurrentWeapon().GetComponent<Melee>();
            int currentAmmo = 0;
            int magSize = 0;
            if (currentWeaponGun is not null) {
                currentAmmo = currentWeaponGun.GetGunData().currentAmmo;
                magSize = currentWeaponGun.GetGunData().magSize;
            } else if(currentWeaponMelee is not null) {
                currentAmmo = 1;
                magSize = 1;
            }
            
            sText.text = currentAmmo + "/" + magSize;
        }
    }
}