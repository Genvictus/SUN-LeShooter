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

        [Header("Audio")]
        [SerializeField] private AudioSource attackSound;

        float timeSinceLastAttack;
        bool effectActive;
        private void Start()
        {
            PlayerAttack.attackInput += Attack;
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

        public void Attack()
        {

            if (CanAttack())
            {
                Debug.Log("Attack2");
                timeSinceLastAttack = 0;

                RaycastHit hit;
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, meleeData.maxDistance))
                {   
                    Debug.Log(hit.transform.name + meleeData.damage);
                    IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();
                    damageAble?.TakeDamage((int)Math.Round(meleeData.damage), hit.transform.position);

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

        public void DisableEffects()
        {
            lineRenderer.enabled = false;
        }

        public void DebuffAttack(float debuff)
        {
            meleeData.damage /= debuff;
        }

        public void BuffAttack(float buff)
        {
            meleeData.damage *= buff;
        }

        public MeleeData GetMeleeData() {
            return meleeData;
        }

        public void ResetMelee() {
            StopAllCoroutines();
            DisableEffects();
        }
    }

}
