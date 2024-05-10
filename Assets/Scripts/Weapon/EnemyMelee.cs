using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MeleeData meleeData;

    [Header("Visuals")]
    [SerializeField] private float visualDuration = 0.1f;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Audio")]
    [SerializeField] private AudioSource attackSound;
    float timeSinceLastAttack;
    bool effectActive;

    public int buffCount = 0;
    private void Start()
    {
        EnemyStrike enemyStrike = GetComponentInParent<EnemyStrike>();
        if (enemyStrike != null)
        {
            enemyStrike.attackAction += Attack;
            enemyStrike.clearAction += DisableEffects;
        }
    }

    void Update()
    {
        if (effectActive && timeSinceLastAttack > visualDuration)
        {
            DisableEffects();
            effectActive = false;
        }
        timeSinceLastAttack += Time.deltaTime;
    }

    private bool CanAttack()
    {
        return timeSinceLastAttack > meleeData.fireRate;
    }

    public void Attack(Transform enemyTransform)
    {

        if (CanAttack())
        {
/*            Debug.Log("Enemy Attack2");*/
            timeSinceLastAttack = 0;

            RaycastHit hit;
            if (Physics.Raycast(enemyTransform.position, enemyTransform.forward, out hit, meleeData.maxDistance))
            {
/*                Debug.Log("Enemy Hit2");
                Debug.Log(hit.transform.name);*/
                IPlayerDamageAble damageAble = hit.transform.GetComponent<IPlayerDamageAble>();

                Debug.Log("Damage before buff " + meleeData.damage);
                float damage = meleeData.damage + (buffCount * 0.2f * meleeData.damage);
                Debug.Log("Damage after buff " + damage);

                damageAble?.TakeDamage(damage, hit.transform.position);

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
        lineRenderer.enabled = true;
        attackSound.Play();
        effectActive = true;
    }

    private void DisableEffects()
    {
        lineRenderer.enabled = false;
    }

}
