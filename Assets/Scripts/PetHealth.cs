using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    public AudioClip deathClip;
    public bool godMode = false;

    int currentHealth;
    bool isDead;
    AudioSource petAudio;
    bool damaged;


    private void Awake()
    {
        petAudio = GetComponent<AudioSource>();
        ResetPet();
    }

    public void ResetPet()
    {
        // Set the initial health of the player.
        currentHealth = maxHealth;

        /*playerMovement.enabled = true;
        playerShooting.enabled = true;

        anim.SetBool("IsDead", false);*/
    }

    public void TakeDamage(int amount)
    {
        if (godMode)
            return;

        damaged = true;

        currentHealth -= amount;

        // healthSlider.value = currentHealth;

        petAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        petAudio.clip = deathClip;
        petAudio.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            Debug.Log("Damaged");
        }

        damaged = false;
    }
}
