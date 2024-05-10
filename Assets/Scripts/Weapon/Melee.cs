using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class Melee : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MeleeData meleeData;
        [SerializeField] private Transform cameraTransform;

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
            PlayerAttack.attackInput += Attack;
            anim = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (effectActive && timeSinceLastAttack > visualDuration)
            {
                DisableEffects();
                effectActive = false;
                anim.ResetTrigger("Attack");
            }
            timeSinceLastAttack += Time.deltaTime;
        }

        private bool CanAttack()
        {
            return timeSinceLastAttack > meleeData.fireRate;
        }

        public void Attack()
        {
            if (gameObject.activeSelf && CanAttack())
            {
                Debug.Log("Attack2");
                timeSinceLastAttack = 0;

                RaycastHit[] hits = Physics.RaycastAll(cameraTransform.position, cameraTransform.forward, meleeData.maxDistance);

                if (hits.Length > 0)
                {
                    foreach (RaycastHit hit in hits)
                    {
                        Debug.Log(hit.transform.name + meleeData.damage);
                        IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();

                        float damage = PlayerShooting.Calculatedamage(meleeData.damage);
                        damageAble?.TakeDamage(damage, hit.point);

                        lineRenderer.SetPosition(0, transform.position);
                        lineRenderer.SetPosition(1, hit.point);
                    }
                }
                else
                {
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

        public void DisableEffects()
        {
            lineRenderer.enabled = false;
        }

        public MeleeData GetMeleeData()
        {
            return meleeData;
        }

        public void ResetMelee()
        {
            StopAllCoroutines();
            DisableEffects();
        }
    }

}
