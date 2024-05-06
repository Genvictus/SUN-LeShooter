using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
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
        Debug.Log("Shoo1t");

        if (gunData.currentAmmo > 0 && CanShoot())
        {
            Debug.Log("Shoot");
            timeSinceLastShot = 0;
            gunData.currentAmmo--;
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.maxDistance))
            {   
                Debug.Log(hit.transform.name + gunData.damage);
                IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();
                damageAble?.Damage(gunData.damage);

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

    public void StartReload()
    {
        Debug.Log("Reload");
        if(!gunData.reloading && gameObject.activeSelf)
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
        gunData.damage /= debuff;
    }

    public void BuffAttack(float buff)
    {
        gunData.damage *= buff;
    }

}
