using UnityEngine;
using System.Collections;
using System;

namespace Nightmare
{
    public class EnemyShoot : PausibleObject
    {
        float timeBetweenAttacks;
        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        public Action shootAction;
        public Action clearRenders;
        bool playerInRange;
        float timer;
        public GunData gunData;


        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();
            timeBetweenAttacks = gunData.fireRate;
            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
            clearRenders?.Invoke();
        }

        // void OnTriggerEnter (Collider other)
        // {
        //     // If the entering collider is the player...
        //     if(other.gameObject == player)
        //     {
        //         // ... the player is in range.
        //         playerInRange = true;
        //     }
        // }

        // void OnTriggerExit (Collider other)
        // {
        //     // If the exiting collider is the player...
        //     if(other.gameObject == player)
        //     {
        //         // ... the player is no longer in range.
        //         playerInRange = false;
        //     }
        // }

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

        void Update ()
        {
            if (isPaused)
                return;
            
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
            CheckPlayerInRange();
            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.CurrentHealth() > 0)
            {
                // ... attack.
                transform.LookAt(player.transform);
                Attack ();
            }

            if (timer >= 0.2f){
                clearRenders?.Invoke();
            }

            // If the player has zero or less health...
            if(playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }
        }

        void Attack ()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                Debug.Log("Enemy has shot");
                shootAction?.Invoke();
                // playerHealth.TakeDamage ((int)gunData.damage, player.transform.position);
            }
        }
    }
}