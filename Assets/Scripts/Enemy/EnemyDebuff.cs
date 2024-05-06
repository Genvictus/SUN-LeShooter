using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyDebuff : PausibleObject
    {
        public float attackDebuff = 0.5f;
        public float moveDebuff = 0.5f;

        public int debuffRange = 10;

        public float debuffRate = 0.5f;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        PlayerMovement playerMovement;
        EnemyHealth enemyHealth;
        Gun gun;
        Melee melee;
        bool playerInRange;
        float timer;

        bool isDebuffed = false;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            playerMovement = player.GetComponent <PlayerMovement> ();

            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();

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
        }

        void OnTriggerExit (Collider other)
        {
            // If the exiting collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is no longer in range.
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
            if(timer >= debuffRate && playerInRange && enemyHealth.CurrentHealth() > 0)
            {
                if (!isDebuffed)
                    Debuff ();

                isDebuffed = true;
            }

            if (timer >= debuffRate && !playerInRange && isDebuffed)
            {   

                Rebuff();
                isDebuffed = false;
            }

            // If the player has zero or less health...
            if(playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }
        }

        void Debuff ()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerMovement.speed *= moveDebuff;

                gun.DebuffAttack(attackDebuff);

                melee.DebuffAttack(attackDebuff);

            }
        }

        void Rebuff ()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {

                playerMovement.speed /= moveDebuff;

                gun.BuffAttack(attackDebuff);

                melee.BuffAttack(attackDebuff);

            }
        }
    }
}