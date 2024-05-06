using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGizmos : MonoBehaviour
{
    public float sightRange, attackRange;
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
