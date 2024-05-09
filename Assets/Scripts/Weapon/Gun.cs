using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Nightmare
{
    public class Gun : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GunData gunData;
        [SerializeField] private Transform muzzle;
        [SerializeField] private Transform cameraTransform;

        [Header("Visuals")]
        [SerializeField] private float visualDuration = 0.2f;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Light muzzleFlash;

        [Header("Audio")]
        [SerializeField] private AudioSource shootSound;
        [SerializeField] private AudioSource reloadSound;

        private int bulletsSpread = 5;

        private List<LineRenderer> lineRenderers;


        float timeSinceLastShot;
        private void Start()
        {
            PlayerShooting.shootInput += Shoot;
            PlayerShooting.reloadInput += StartReload;
            if (gunData.spread){
                lineRenderers = new List<LineRenderer>();
                initLineRenders();           
            }
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
            if (gunData.currentAmmo > 0 && CanShoot())
            {
                Debug.Log("Shoot");
                timeSinceLastShot = 0;
                gunData.currentAmmo--;
                OnGunShot();
                if (gunData.spread)
                {
                    SpreadShoot();
                }
                else
                {
                    DefaultShoot();
                }
                StartCoroutine(DisableEffects(visualDuration));
            }
        }

        private void DefaultShoot(){
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.maxDistance))
            {
                Debug.Log("got hit: " + hit.transform.name);
                IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();
                damageAble?.TakeDamage((int)Math.Round(gunData.damage), hit.transform.position);

                lineRenderer.SetPosition(0, muzzle.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                lineRenderer.SetPosition(0, muzzle.position);
                lineRenderer.SetPosition(1, muzzle.position + muzzle.forward * gunData.maxDistance);
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
        private void SpreadShoot()
        {
            float spreadAngle = 20f; 

            for (int i = 0; i <= bulletsSpread; i++)
            {
                Vector3 spreadDirection = Quaternion.Euler(UnityEngine.Random.Range(-spreadAngle, spreadAngle), UnityEngine.Random.Range(-spreadAngle, spreadAngle), 0) * cameraTransform.forward;

                LineRenderer newLineRenderer = lineRenderers[i];

                if (Physics.Raycast(cameraTransform.position, spreadDirection, out RaycastHit hit, gunData.maxDistance))
                {
                    Debug.Log("Got hit: " + hit.transform.name);
                    IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();

                    float distance = Vector3.Distance(muzzle.position, hit.point);
                    float adjustedDamage = CalculateAdjustedDamage(distance);

                    damageAble?.TakeDamage((int)adjustedDamage, hit.point);

                    newLineRenderer.SetPosition(0, muzzle.position);
                    newLineRenderer.SetPosition(1, hit.point);
                } else {
                    newLineRenderer.SetPosition(0, muzzle.position);
                    newLineRenderer.SetPosition(1, muzzle.position + spreadDirection * gunData.maxDistance);
                }
            }

            StartCoroutine(DisableLineRenderers(lineRenderers));
        }

        private float CalculateAdjustedDamage(float distance)
        {
            float maxDistance = gunData.maxDistance; 
            float minDistance = 0f;
            float maxDamage = gunData.damage;
            float minDamage = 1f;

            float slope = (maxDamage - minDamage) / (maxDistance - minDistance);

            float adjustedDamage = slope * distance + maxDamage;

            adjustedDamage = Mathf.Max(minDamage, adjustedDamage);

            return adjustedDamage;
        }


        private IEnumerator DisableLineRenderers(List<LineRenderer> lineRenderers)
        {
            yield return new WaitForSeconds(visualDuration);

            foreach (LineRenderer renderer in lineRenderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = false;
                }
            }
        }

        private IEnumerator DisableEffects(float duration)
        {
            yield return new WaitForSeconds(duration);
            DisableEffects();
        }

        private void OnGunShot()
        {
            lineRenderer.enabled = true;
            muzzleFlash.enabled = true;
            shootSound.Play();

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
        }

        public void StartReload()
        {
            Debug.Log("Reload");
            if (!gunData.reloading && gameObject.activeSelf)
                {
                    reloadSound.Play();
                    StartCoroutine(Reload());
                
                }
        }

        private IEnumerator Reload()
        {
            gunData.reloading = true;
            yield return new WaitForSeconds(gunData.reloadTime);
            gunData.currentAmmo = gunData.magSize;
            gunData.reloading = false;
        }

        public void DebuffAttack(float debuff)
        {
            gunData.damage /= debuff;
        }

        public void BuffAttack(float buff)
        {
            gunData.damage *= buff;
        }

        public GunData GetGunData() {
            return gunData;
        }
    }
}
