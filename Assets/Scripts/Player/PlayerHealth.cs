using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class PlayerHealth : MonoBehaviour, IPlayerDamageAble
    {
        public float startingHealth = 100;
        public float maxHealth = 100;
        public float currentHealth;
        public AudioClip deathClip;
        public bool godMode = false;
        public bool damaged;

        Animator anim;
        AudioSource playerAudio;
        PlayerMovement playerMovement;
        PlayerShooting playerShooting;
        bool isDead;

        void Awake()
        {
            // Setting up the references.
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            playerShooting = GetComponentInChildren<PlayerShooting>();

            ResetPlayer();
        }

        public void ResetPlayer()
        {
            // Set the initial health of the player.
            currentHealth = startingHealth;

            playerMovement.enabled = true;
            playerShooting.enabled = true;

            anim.SetBool("IsDead", false);
            isDead = false;
        }


        void Update()
        {
        }


        public void TakeDamage(float amount, Vector3 hitPoint)
        {
            if (godMode)
                return;

            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount * DifficultyManager.GetIncomingDamageRate();

            // Play the hurt sound effect.
            playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }

        public void Heal(float amount)
        {
            if (!isDead)
            {
                currentHealth += amount;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }

        void Death()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;

            // Turn off any remaining shooting effects.
            // playerShooting.DisableEffects();

            // Tell the animator that the player is dead.
            anim.SetBool("IsDead", true);

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.clip = deathClip;
            playerAudio.Play();

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }

        public void RestartLevel()
        {
            EventManager.TriggerEvent("GameOver");
        }
    }
}