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
        [SerializeField] private LineRenderer lineRenderer;

        float timeSinceLastShot;
        private void Start()
        {
            PlayerShooting.shootInput += Shoot;
            PlayerShooting.reloadInput += StartReload;
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
                OnGunShot();
            }

        }

        private void OnGunShot()
        {

        }

        public void StartReload()
        {
            Debug.Log("Reload");
            if (!gunData.reloading)
                StartCoroutine(Reload());
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
            gunData.damage *= debuff;
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