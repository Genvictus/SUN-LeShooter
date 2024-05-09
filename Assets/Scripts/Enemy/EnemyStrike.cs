using System;
using Nightmare;
using UnityEngine;

namespace Nightmare
{
    public class EnemyStrike : EnemyAttack
    {
        public MeleeData meleeData;

        protected override void Awake()
        {
            // Setting up the references.
            timeBetweenAttacks = meleeData.fireRate;
            attackRange = meleeData.maxDistance;
            base.Awake();
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
            if (other.gameObject == pet)
            {
                // ... the pet is in range.
                petInRange = false;
            }

        }
    }

}