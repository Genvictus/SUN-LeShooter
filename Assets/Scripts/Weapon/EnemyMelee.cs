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
    Animator anim;

    [Header("Audio")]
    [SerializeField] private AudioSource attackSound;
    float timeSinceLastAttack;
    bool effectActive;
    private void Start()
    {
        EnemyStrike enemyStrike = GetComponentInParent<EnemyStrike>();
        if (enemyStrike != null)
        {
            enemyStrike.attackAction += Attack;
            enemyStrike.clearAction += DisableEffects;
        }
        anim = GetComponentInChildren<Animator>();
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
            Debug.Log("Enemy Attack2");
            timeSinceLastAttack = 0;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, meleeData.maxDistance))
            {
                IPlayerDamageAble damageAble = hit.transform.GetComponent<IPlayerDamageAble>();
                damageAble?.TakeDamage(meleeData.damage, hit.transform.position);

                Debug.Log(hit.transform.name + " " + meleeData.damage);

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward * meleeData.maxDistance);
            }
            OnMeleeAttack();
            anim.SetTrigger("Attack");
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
