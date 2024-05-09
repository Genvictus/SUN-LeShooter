﻿using UnityEngine;
using System.Collections;
using System;

namespace Nightmare
{
    public abstract class EnemyAttack : PausibleObject
    {
        protected float timeBetweenAttacks;
        protected float attackRange;
        protected Animator anim;

        protected GameObject player;
        protected PlayerHealth playerHealth;
        protected GameObject pet;
        protected PetHealth petHealth;
        protected bool playerInRange;
        protected bool petInRange;

        protected EnemyHealth enemyHealth;
        protected float timer;

        protected virtual void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();

            pet = GameObject.FindGameObjectWithTag("Pet");
            if (pet is not null)
            {
                petHealth = pet.GetComponent<PetHealth>();
            }


            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        protected void CheckTargetInRange()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
            {
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }
            if (Vector3.Distance(pet.transform.position, transform.position) < attackRange)
            {
                petInRange = true;
            }
            else
            {
                petInRange = false;
            }
        }

        protected void Update ()
        {
            if (isPaused)
                return;

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= timeBetweenAttacks && enemyHealth.CurrentHealth() > 0)
            {
                CheckTargetInRange();
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

            // If the player has zero or less health...
            if (playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }
        }

        protected abstract Action<Transform> GetAttackAction();

        protected void AttackPlayer ()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                GetAttackAction()?.Invoke(player.transform);
                // playerHealth.TakeDamage (attackDamage);
            }
        }
        protected void AttackPet()
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (petHealth.currentHealth > 0)
            {
                // ... damage the pet.
                GetAttackAction()?.Invoke(pet.transform);
            }
        }
    }
}