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
        [SerializeField] private LineRenderer lineRenderer;

        float timeSinceLastAttack;
        private void Start()
        {
            PlayerAttack.attackInput += Attack;
        }

        void Update()
        {
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

                    float damage = meleeData.damage;
                    damage += meleeData.damage * PlayerShooting.orbBuffMultiplier * PlayerShooting.orbBuffCount;
                    damage *= PlayerShooting.mobDebuff;
                    damageAble?.TakeDamage(damage, hit.transform.position);

                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, transform.position + transform.forward * meleeData.maxDistance);
                }
                OnMeleeAttack();
            }

        }

        private void OnMeleeAttack()
        {

        }

        public MeleeData GetMeleeData()
        {
            return meleeData;
        }
    }
}
