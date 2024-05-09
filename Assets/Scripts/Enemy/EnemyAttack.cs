using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        bool playerInRange;
        bool petInRange;
        float timer;
        GameObject pet;
        PetHealth petHealth;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();

            pet = GameObject.FindGameObjectWithTag("Pet");
            if(pet is not null)
            {
                petHealth = pet.GetComponent<PetHealth>();
            }
            

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
                // ... the player is in range.
                playerInRange = false;
            }
            if (other.gameObject == pet)
            {
                // ... the player is in range.
                petInRange = false;
                // Debug.Log("pet in range");
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
                if(playerInRange)
                {
                    AttackPlayer();
                }
                else if (petInRange)
                {
                    AttackPet();
                }
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
                playerHealth.TakeDamage (attackDamage);
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