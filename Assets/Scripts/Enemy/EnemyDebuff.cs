using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class EnemyDebuff : PausibleObject
    {
        public float attackDebuff = 2f;
        public float moveDebuff = 2f;
        public int debuffRange = 5;
        public float debuffRate = 1f;
        GameObject player;
        PlayerHealth playerHealth;
        PlayerMovement playerMovement;
        EnemyHealth enemyHealth;
        WeaponSwitching weaponSwitching;
        Gun Default;
        Gun Shotgun;
        Melee Sword;
        bool playerInRange;
        float timer;
        bool isDebuffed = false;

        void Start ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            playerMovement = player.GetComponent <PlayerMovement> ();

            weaponSwitching = player.GetComponentInChildren<WeaponSwitching>();

            Default = weaponSwitching.GetWeapon(0).GetComponent<Gun>();
            Shotgun = weaponSwitching.GetWeapon(1).GetComponent<Gun>();
            Sword = weaponSwitching.GetWeapon(2).GetComponent<Melee>();

            enemyHealth = GetComponent<EnemyHealth>();

            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void CheckInRange(){
            if(Vector3.Distance(player.transform.position, transform.position) < debuffRange){
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
            if(timer >= debuffRate && playerInRange && enemyHealth.CurrentHealth() > 0)
            {
                if (!isDebuffed)
                    Debuff ();

            }

            if (timer >= debuffRate && !playerInRange && isDebuffed)
            {   

                Rebuff();
            }
        }

        void Debuff ()
        {
            // Reset the timer.
            timer = 0f;
            isDebuffed = true;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                playerMovement.speed /= moveDebuff;
                playerMovement.walkSpeed /= moveDebuff;
                playerMovement.runSpeed /= moveDebuff;

                Default.DebuffAttack(attackDebuff);

                Sword.DebuffAttack(attackDebuff);

                Shotgun.DebuffAttack(attackDebuff);

            }
        }

        void Rebuff ()
        {
            // Reset the timer.
            timer = 0f;
            isDebuffed = false;

            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {

                playerMovement.speed *= moveDebuff;
                playerMovement.walkSpeed *= moveDebuff;
                playerMovement.runSpeed *= moveDebuff;

                Default.BuffAttack(attackDebuff);

                Sword.BuffAttack(attackDebuff);

                Shotgun.BuffAttack(attackDebuff);

            }
        }
    }
}