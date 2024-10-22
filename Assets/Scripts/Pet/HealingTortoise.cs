using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Nightmare
{
    public class HealingTortoise : PausibleObject
    {
        // Start is called before the first frame update
        public NavMeshAgent pet;
        public Transform target;
        public GameObject player;
        bool playerInRange;
        public PlayerHealth health;
        public float healCooldown;
        public int healAmount;
        float _timer;

        AudioSource _audio;
        PetHealth petHealth;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            petHealth = GetComponent<PetHealth>();
            health = player.GetComponent<PlayerHealth>();
            _timer = 0f;
            _audio = GetComponent<AudioSource>();
            _audio.Play();
            if ((transform.position - target.position).magnitude <= 100)
            {
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused || petHealth.isDead)
                return;

            pet.SetDestination(target.position);

            if ((transform.position - target.position).magnitude <= 100)
            {
                playerInRange = true;
                //Debug.Log("player is in range");
            }
            else
            {
                playerInRange = false;
            }

            _timer += Time.deltaTime;
            if (playerInRange && _timer >= healCooldown && health.currentHealth < 100)
            {
                _audio.Play();
                health.Heal(healAmount);
                _timer = 0f;
            }
        }

        void OnDestroy()
        {
            StopPausible();
        }
    }
}

