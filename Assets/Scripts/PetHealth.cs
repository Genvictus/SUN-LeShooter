using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class PetHealth : MonoBehaviour, IPlayerDamageAble
{
    // Start is called before the first frame update
    public int maxHealth;
    public AudioClip deathClip;
    public bool godMode = false;
    public float sinkSpeed = 0.1f;

    public float currentHealth;
    public bool isDead;
    AudioSource petAudio;
    bool damaged;
    CapsuleCollider capsuleCollider;


    private void Awake()
    {
        petAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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

    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (godMode)
            return;

        damaged = true;

        currentHealth -= amount * DifficultyManager.GetIncomingDamageRate();

        // healthSlider.value = currentHealth;

        petAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("pet is dead :(");
        isDead = true;

        StartSinking();
        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        petAudio.clip = deathClip;
        petAudio.Play();
    }

    private void SetKinematics(bool isKinematic)
    {
        capsuleCollider.isTrigger = isKinematic;
        capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        SetKinematics(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            // Debug.Log(transform.position.y);
            if (transform.position.y < -10f)
            {
                Destroy(this.gameObject);
            }
        }

        damaged = false;
    }
}
