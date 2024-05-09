using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class WeaponSwitching : PausibleObject {

    [Header("References")]
    [SerializeField] private Transform[] weapons = new Transform[3]; 

    [Header("Settings")]
    [SerializeField] private float switchTime;

    public int selectedWeapon;
    private float timeSinceLastSwitch;

    void Awake()
    {
        StartPausible();
    }

    private void Start() {
        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    void OnDestroy()
    {
        StopPausible();
    }

    private void SetWeapons() {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            weapons[i] = transform.GetChild(i);
    }

    private void Update() {
        // bool isReloading = false;
        // if (GetCurrentWeapon().GetComponent<Gun>() != null){
        //     isReloading = GetCurrentWeapon().GetComponent<Gun>().GetGunData().reloading;
        // }

        if (isPaused)
            return;

        int previousSelectedWeapon = selectedWeapon;

        // Check for number key input 3 (default) weapons (1, 2, 3)
        for (int i = 0; i < weapons.Length; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && timeSinceLastSwitch >= switchTime) {
                selectedWeapon = i;
                break;
            }
        }

        // Check for scroll wheel input
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll != 0 && timeSinceLastSwitch >= switchTime) {
         selectedWeapon += (scroll > 0) ? -1 : 1;
            if (selectedWeapon < 0)
                selectedWeapon = weapons.Length - 1;
            else if (selectedWeapon >= weapons.Length)
                selectedWeapon = 0;
        }


        if (previousSelectedWeapon != selectedWeapon) 
            ClearPreviousWeapon(previousSelectedWeapon);
            Select(selectedWeapon);

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void Select(int weaponIndex) {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == weaponIndex);

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();
    }

    private void OnWeaponSelected() {  }

    private void ClearPreviousWeapon(int previousSelectedWeapon) {
        GameObject previousWeapon = GetWeapon(previousSelectedWeapon);

        if (previousSelectedWeapon == 3) {
            Melee meleeComponent = previousWeapon.GetComponent<Melee>();
            if (meleeComponent != null)
                meleeComponent.ResetMelee();
        } else {
            Gun gunComponent = previousWeapon.GetComponent<Gun>();
            if (gunComponent != null)
                gunComponent.ResetGun();
        }
    }


    public GameObject GetCurrentWeapon() {
        return weapons[selectedWeapon].gameObject;
    }

    public GameObject GetWeapon(int idx) {
        return weapons[idx].gameObject;
    }
}
