using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    private float damage = 10;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    void OnCollisionEnter(Collision collision)
    {
        IMobs enemyHealth = collision.gameObject.GetComponent<IMobs>();
        enemyHealth?.TakeDamage(damage);

        Destroy(gameObject);
    }
}
