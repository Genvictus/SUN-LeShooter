using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IMobs, IDamageAble
{
    public float Health { get; set; }
    public EnemyManager enemyManager;

    void Start()
    {
        Health = 50;

        enemyManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<EnemyManager>();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy Get damaged");

        Health -= damage;
        
        if (Health <= 0)
            DestroyEnemy();
    }
    
    public void TakeDamage(int damage, Vector3 hitpoint)
    {
        TakeDamage(damage);
    }

    void DestroyEnemy()
    {
        enemyManager.EnemyDefeated();
        Destroy(gameObject);
    }
}
