using System.Collections;
using System.Collections.Generic;
using Nightmare;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class EnemyGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform enemyTransform;

    [Header("Visuals")]
    [SerializeField] private float visualDuration = 0.2f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Light muzzleFlash;

    [Header("Audio")]
    [SerializeField] private AudioSource shootSound;

    float timeSinceLastShot;
    private int bulletsSpread = 3;

    private void Start()
    {
        EnemyShoot.shootAction += Shoot;
    }

    void Update()
    {
        if (gunData.shooting && timeSinceLastShot > visualDuration)
        {
            DisableEffects();
            gunData.shooting = false;
        }
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward * gunData.maxDistance, Color.red);
    }

    private bool CanShoot()
    {
        return !gunData.reloading && timeSinceLastShot > gunData.fireRate;
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Debug.Log("Enemy Shoot");
            timeSinceLastShot = 0;
            RaycastHit hit;
            if (Physics.Raycast(enemyTransform.position, enemyTransform.forward, out hit, gunData.maxDistance))
            {   
                Debug.Log(hit.transform.name);
                IPlayerDamageAble damageAble = hit.transform.GetComponent<IPlayerDamageAble>();
                damageAble?.TakeDamage((int)Math.Round(gunData.damage), hit.transform.position);

                lineRenderer.SetPosition(0, muzzle.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else {
                lineRenderer.SetPosition(0, muzzle.position);
                lineRenderer.SetPosition(1, muzzle.position + muzzle.forward * gunData.maxDistance);
            }
            OnGunShot();
        }


    }

    private void OnGunShot()
    {
        lineRenderer.enabled = true;
        muzzleFlash.enabled = true;
        shootSound.Play();
        gunData.shooting = true;
    }


    private void DisableEffects()
    {
        lineRenderer.enabled = false;
        muzzleFlash.enabled = false;
    }


}
