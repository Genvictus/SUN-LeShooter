using UnityEngine;
using System.Collections;
using System;

namespace Nightmare
{
    public class EnemyAttack : PausibleObject
    {
        float timeBetweenAttacks;
        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        public static Action attackAction;
        bool playerInRange;
        bool petInRange;
        float timer;
        public MeleeData meleeData;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();
            timeBetweenAttacks = meleeData.fireRate;

            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void OnTriggerEnter (Collider other)
        {
            // If the entering collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
            }
            if (other.gameObject == pet)
            {
                // ... the pet is in range.
                petInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            // If the exiting collider is the player...
            if (other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }


        void CheckPlayerInRange()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < meleeData.maxDistance)
            {
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }
        }

        void Update ()
        {
            if (isPaused)
                return;

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= timeBetweenAttacks && enemyHealth.CurrentHealth() > 0)
            {
                // ... attack.
                transform.LookAt(player.transform);
                Attack ();
            }

            // If the player has zero or less health...
            if(playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }
        }

        void AttackPlayer ()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                attackAction?.Invoke();
                // playerHealth.TakeDamage (attackDamage);
            }
        }
        void AttackPet()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (petHealth.currentHealth > 0)
            {
                // ... damage the player.
                petHealth.TakeDamage(attackDamage);
            }
            
        }
    }
}