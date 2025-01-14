using UnityEngine;
using System.Collections;
using System;

namespace Nightmare
{
    public class EnemyShoot : EnemyAttack
    {
        public GunData gunData;
        public bool rageAble = false;
        bool rage;
        public AudioSource rageSound;

        protected override void Awake()
        {
            // Setting up the references.
            timeBetweenAttacks = gunData.fireRate;
            attackRange = gunData.maxDistance;
            base.Awake();
            if (rageAble)
            {
                anim = GetComponentInChildren<Animator>();
            }
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void CheckPlayerInRange()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < gunData.maxDistance)
            {
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }
        }

        void CheckPetInRange()
        {
            if (pet != null && Vector3.Distance(pet.transform.position, transform.position) < gunData.maxDistance)
            {
                petInRange = true;
            }
            else
            {
                petInRange = false;
            }
        }

        new void Update()
        {
            if (isPaused)
                return;

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            CheckPlayerInRange();
            CheckPetInRange();

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && enemyHealth.CurrentHealth() > 0)
            {

                if (enemyHealth.PercentageHealth() < 0.3f && rageAble && !rage)
                {
                    Rage();
                }

                // ... attack.
                if (playerInRange)
                {
                    transform.LookAt(player.transform);
                    AttackPlayer();
                }
                else if (petInRange)
                {
                    transform.LookAt(pet.transform);
                    AttackPet();
                }
            }

            if (timer >= 0.1f)
            {
                clearAction?.Invoke();
            }

            // If the player has zero or less health...
            if (playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger("PlayerDead");
            }
        }

        private void Rage()
        {
            Debug.Log("Rage");
            anim.SetTrigger("Rage");
            if (rageSound == null){
                Debug.LogError("Rage sound not found");
            }
            rageSound.Play();
            rage = true;
            timeBetweenAttacks = gunData.fireRate / 2;
        }
    }
}