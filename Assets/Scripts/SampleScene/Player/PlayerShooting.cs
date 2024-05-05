using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject projectile;
    public int damagePerShot = 20;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 bulletSpawnPostion = transform.position + transform.forward * 1.5f;

        Rigidbody rb = Instantiate(projectile, bulletSpawnPostion, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 1.5f, ForceMode.Impulse);

        Destroy(rb.gameObject, 2f);

        Debug.Log("Player has shot");
    }
}
