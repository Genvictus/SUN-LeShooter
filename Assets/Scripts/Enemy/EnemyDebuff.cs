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
        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        PlayerMovement playerMovement;
        EnemyHealth enemyHealth;
        public Gun Default;
        public Gun Shotgun;
        public Melee Sword;
        bool playerInRange;
        float timer;
        bool isDebuffed = false;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            playerMovement = player.GetComponent <PlayerMovement> ();

            GameObject DefaultObject = GameObject.FindGameObjectWithTag("Default");
            Default = DefaultObject.GetComponent<Gun>();

            GameObject ShotgunObject = GameObject.FindGameObjectWithTag("Shotgun");
            Shotgun = ShotgunObject.GetComponent<Gun>();

            GameObject SwordObject = GameObject.FindGameObjectWithTag("Sword");
            Sword = SwordObject.GetComponent<Melee>();


            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();

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

                isDebuffed = true;
            }

            if (timer >= debuffRate && !playerInRange && isDebuffed)
            {   

                Rebuff();
                isDebuffed = false;
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