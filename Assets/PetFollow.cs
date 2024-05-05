using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Nightmare
{
    public class PetFollow : MonoBehaviour
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

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
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
                Debug.Log("Playing heal sound");
                _audio.Play();
                Debug.Log("Heal sound should have played");
                health.Heal(healAmount);
                _timer = 0f;
            }
        }
    }
}

