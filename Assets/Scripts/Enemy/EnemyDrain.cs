using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyDrain : PausibleObject
    {
        public int drainAmount = 1;
        public float drainRate =  5f;
        public int drainRange = 3;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        bool playerInRange;
        float timer;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();

            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void CheckInRange(){
            if(Vector3.Distance(player.transform.position, transform.position) < drainRange){
                playerInRange = true;
            } else {
                playerInRange = false;
            }
        }

        void Update ()
        {
            if (isPaused)
                return;
            
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
            CheckInRange();
            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= drainRate && playerInRange && enemyHealth.CurrentHealth() > 0)
            {
                // ... attack.
                Drain ();
            }

            // If the player has zero or less health...
            if(playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }
        }

        void Drain ()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerHealth.TakeDamage (drainAmount, player.transform.position);
            }
        }
    }
}