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
    [SerializeField] private LineRenderer lineRenderer;

    float timeSinceLastShot;
    private void Start()
    {
        EnemyShoot.shootAction += Shoot;
    }

    void Update()
    {
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
            // Debug.Log("Enemy Shoot");
            timeSinceLastShot = 0;
            RaycastHit hit;
            if (Physics.Raycast(enemyTransform.position, enemyTransform.forward, out hit, gunData.maxDistance))
            {   
                Debug.Log(hit.transform.name);
                IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();
                damageAble?.TakeDamage(gunData.damage, hit.transform.position);

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

    }

}
