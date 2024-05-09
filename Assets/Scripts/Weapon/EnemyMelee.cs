using System;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
   [Header("References")]
    [SerializeField] private MeleeData meleeData;
    [SerializeField] private Transform enemyTransform;

    [Header("Visuals")]
    [SerializeField] private LineRenderer lineRenderer;

    float timeSinceLastAttack;
    private void Start()
    {
        EnemyAttack.attackAction += Attack;
    }

    void Update()
    {
        Debug.DrawLine(enemyTransform.position, enemyTransform.position + enemyTransform.forward * meleeData.maxDistance, Color.red);
        timeSinceLastAttack += Time.deltaTime;
    }

    private bool CanAttack()
    {
        return timeSinceLastAttack > meleeData.fireRate;
    }

    public void Attack()
    {

        if (CanAttack())
        {
            Debug.Log("Enemy Attack2");
            timeSinceLastAttack = 0;

            RaycastHit hit;
            if (Physics.Raycast(enemyTransform.position, enemyTransform.forward, out hit, meleeData.maxDistance))
            {   
                Debug.Log("Enemy Hit2");
                Debug.Log(hit.transform.name);
                IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();
                damageAble?.TakeDamage(meleeData.damage, hit.transform.position);

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward * meleeData.maxDistance);
            }
            OnMeleeAttack();
        }

    }

    private void OnMeleeAttack()
    {

    }

}
