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
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    private void Start()
    {
        EnemyShoot.shootAction += Shoot;
        if (gunData.spread && lineRenderers.Count == 0){
            initLineRenders();           
        }
    }

     private void initLineRenders(){
            Material originalMaterial = lineRenderer.sharedMaterial;
            float originalStartWidth = lineRenderer.startWidth;
            float originalEndWidth = lineRenderer.endWidth;

            for (int i = 0; i <= bulletsSpread; i++)
            {
                GameObject lineObject = new GameObject("Line" + i);
                LineRenderer newLineRenderer = lineObject.AddComponent<LineRenderer>();

                newLineRenderer.enabled = false;

                newLineRenderer.sharedMaterial = originalMaterial;
                newLineRenderer.startWidth = originalStartWidth;
                newLineRenderer.endWidth = originalEndWidth;

                newLineRenderer.positionCount = 2;

                lineRenderers.Add(newLineRenderer);
            }
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
            timeSinceLastShot = 0;
            OnGunShot();

            if (gunData.spread)
            {
                SpreadShoot();
            }
            else
            {
                DefaultShoot();
            }
        }

    }

    private void DefaultShoot(){
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
    }

    private void SpreadShoot()
    {
        float spreadAngle = 20f;

        for (int i = 0; i <= bulletsSpread; i++)
        {
            Vector3 spreadDirection = Quaternion.Euler(UnityEngine.Random.Range(-spreadAngle, spreadAngle), UnityEngine.Random.Range(-spreadAngle, spreadAngle), 0) * muzzle.forward;

            LineRenderer newLineRenderer = lineRenderers[i];

            if (Physics.Raycast(muzzle.position, spreadDirection, out RaycastHit hit, gunData.maxDistance))
            {
                IPlayerDamageAble damageAble = hit.transform.GetComponent<IPlayerDamageAble>();

                float distance = Vector3.Distance(muzzle.position, hit.point);
                float adjustedDamage = CalculateAdjustedDamage(distance);

                damageAble?.TakeDamage((int)adjustedDamage, hit.point);

                newLineRenderer.SetPosition(0, muzzle.position);
                newLineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                newLineRenderer.SetPosition(0, muzzle.position);
                newLineRenderer.SetPosition(1, muzzle.position + spreadDirection * gunData.maxDistance);
            }
        }
    }

    private float CalculateAdjustedDamage(float distance)
    {
        float maxDistance = gunData.maxDistance; 
        float maxDamage = gunData.damage;
        float minDamage = 1f;

        float t = distance / maxDistance;
        float adjustedDamage = Mathf.Lerp(maxDamage, minDamage, t);

        return adjustedDamage;
    }


    private void OnGunShot()
    {
        lineRenderer.enabled = true;
        muzzleFlash.enabled = true;
        shootSound.Play();
        gunData.shooting = true;
        if (gunData.spread){
            foreach (LineRenderer renderer in lineRenderers)
            {
                renderer.enabled = true;
            }
        }
    }


    private void DisableEffects()
    {
        lineRenderer.enabled = false;
        muzzleFlash.enabled = false;
        if (gunData.spread){
            DisableLineRenderers(lineRenderers);
        }
    }

    private void DisableLineRenderers (List<LineRenderer> lineRenderers)
    {
        foreach (LineRenderer renderer in lineRenderers)
        {
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }


}