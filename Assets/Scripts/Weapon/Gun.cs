using System.Collections;
using System.Collections.Generic;
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
        private Animator anim;

        [Header("Audio")]
        [SerializeField] private AudioSource shootSound;
        [SerializeField] private AudioSource reloadSound;

        private int bulletsSpread = 8;

        private List<LineRenderer> lineRenderers = new();

        float timeSinceLastShot;
        PlayerShooting shooting;

        private void Start()
        {
            shooting = GetComponentInParent<PlayerShooting>();
            if (shooting != null)
            {
                shooting.shootInput += Shoot;
                shooting.reloadInput += StartReload;
            }
            if (gunData.spread)
            {
                initLineRenders();
            }
            anim = GetComponentInChildren<Animator>();
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
            if (gameObject.activeSelf && gunData.currentAmmo > 0 && CanShoot())
            {
                Debug.Log("Shoot");
                gunData.shooting = true;
                timeSinceLastShot = 0;
                
                if (!PlayerShooting.infiniteBulletMode) {
                    gunData.currentAmmo--;
                }
                
                OnGunShot();
                if (gunData.spread)
                {
                    SpreadShoot();
                }
                else
                {
                    DefaultShoot();
                }
                anim.SetTrigger("Shoot");
            }
        }

        private void DefaultShoot()
        {
            RaycastHit hit;
            StatsManager.playerStats.totalShot++;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.maxDistance))
            {
                Debug.Log("got hit: " + hit.transform.name);
                IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();

                float damage = PlayerShooting.Calculatedamage(gunData.damage);
                Debug.Log("Deal damage: " + damage);
                damageAble?.TakeDamage(damage, hit.transform.position);
                if (damageAble != null) {
                    StatsManager.playerStats.shotHit++;
                }

                lineRenderer.SetPosition(0, muzzle.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                lineRenderer.SetPosition(0, muzzle.position);
                lineRenderer.SetPosition(1, muzzle.position + muzzle.forward * gunData.maxDistance);
            }

        }

        private void initLineRenders()
        {
            DeleteLineRenders();

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
            float spreadAngle = 15f;

            for (int i = 0; i <= bulletsSpread; i++)
            {
                Vector3 spreadDirection = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * cameraTransform.forward;

                LineRenderer newLineRenderer = lineRenderers[i];

                if (Physics.Raycast(cameraTransform.position, spreadDirection, out RaycastHit hit, gunData.maxDistance))
                {
                    Debug.Log("Got hit: " + hit.transform.name);
                    IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();

                    float distance = Vector3.Distance(muzzle.position, hit.point);
                    float adjustedDamage = CalculateAdjustedDamage(distance);

                    damageAble?.TakeDamage(adjustedDamage, hit.point);

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
            float maxDamage = PlayerShooting.Calculatedamage(gunData.damage);
            float minDamage = 1f;

            float t = distance / maxDistance;
            float adjustedDamage = Mathf.Lerp(maxDamage, minDamage, t);

            if (PlayerShooting.godMode)
            {
                adjustedDamage = 6969.69f;
            }

            return adjustedDamage;
        }


        private void DisableLineRenderers()
        {
            foreach (LineRenderer renderer in lineRenderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = false;
                }
            }
        }

        private void DeleteLineRenders()
        {
            foreach (LineRenderer renderer in lineRenderers)
            {
                if (renderer != null)
                {
                    Destroy(renderer.gameObject);
                }
            }
            lineRenderers.Clear();
        }

        private bool LineRenersNull()
        {
            foreach (LineRenderer renderer in lineRenderers)
            {
                if (renderer == null)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnGunShot()
        {
            lineRenderer.enabled = true;
            muzzleFlash.enabled = true;
            shootSound.Play();

            if (gunData.spread)
            {
                if (LineRenersNull()) {
                    initLineRenders();
                }

                foreach (LineRenderer renderer in lineRenderers)
                {
                    renderer.enabled = true;
                }
            }
        }

        public void DisableEffects()
        {
            lineRenderer.enabled = false;
            muzzleFlash.enabled = false;
            if (gunData.spread)
            {
                DisableLineRenderers();
            }
        }

        public void StartReload()
        {
            Debug.Log("Reload");
            if (!gunData.reloading && gameObject.activeSelf)
            {
                reloadSound.Play();
                anim.SetTrigger("Reload");
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

        public GunData GetGunData()
        {
            return gunData;
        }

        public void ResetGun()
        {
            StopAllCoroutines();
            DisableEffects();
            gunData.reloading = false;
        }

        void OnDestroy()
        {
            shooting.shootInput -= Shoot;
            shooting.reloadInput -= StartReload;
            DeleteLineRenders();
        }
    }
}
