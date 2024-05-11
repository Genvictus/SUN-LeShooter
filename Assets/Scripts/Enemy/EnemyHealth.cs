using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Nightmare
{
    public class EnemyHealth : MonoBehaviour, IDamageAble
    {
        public float startingHealth = 100;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public int coinValue = 10;
        public AudioClip deathClip;

        float currentHealth;
        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        EnemyMovement enemyMovement;
        public float orbDropChance = 0.5f;


        void Awake()
        {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            enemyMovement = this.GetComponent<EnemyMovement>();
        }

        void OnEnable()
        {
            currentHealth = startingHealth;
            SetKinematics(false);
        }

        private void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }

        void Update()
        {
            if (IsDead())
            {
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -10f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        public bool IsDead()
        {
            return (currentHealth <= 0f);
        }

        public void TakeDamage(float amount, Vector3 hitPoint)
        {
            Debug.Log("Enemy take damage: " + amount);
            if (!IsDead())
            {
                enemyAudio.Play();
                currentHealth -= amount;

                if (IsDead())
                {
                    Death();
                }
                else
                {
                    enemyMovement.GoToPlayer();
                }
            }
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
        }

        void Death()
        {
            TriggerEvents();
            anim.SetTrigger ("Dead");
            if (name.StartsWith("Keroco"))
            {
                StatsManager.playerStats.kerocoKillCount++;
            }
            if (name.StartsWith("Kepala Keroco"))
            {
                StatsManager.playerStats.kepalaKerocoKillCount++;
            }
            if (name.StartsWith("Jenderal"))
            {
                StatsManager.playerStats.jenderalKillCount++;
            }
            if (name.StartsWith("Raja"))
            {
                StatsManager.playerStats.rajaKillCount++;
            }

            EventManager.TriggerEvent("Sound", this.transform.position);
            anim.SetTrigger("Dead");
            Vector3 orbSpawnPosition = transform.position;
            orbSpawnPosition.y += 0.5f;

            float orbDropValue = Random.value;
            if (orbDropValue <= orbDropChance)
            {
                GameObject[] orbs = Orb.GetOrbs();
                int orbIndex = Random.Range(0, orbs.Length);
                GameObject orb = orbs[orbIndex];
                Instantiate(orb, orbSpawnPosition, Quaternion.identity);
            }

            enemyAudio.clip = deathClip;
            enemyAudio.Play();
        }

        public void StartSinking()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            SetKinematics(true);

            ScoreManager.IncreaseScore(scoreValue);
        }

        private void TriggerEvents()
        {
            EventManager.TriggerEvent("Sound", this.transform.position);

            EventManager.TriggerEvent($"EnemyKilled");
            EventManager.TriggerEvent($"{this.gameObject.name}Killed");
            EventManager.TriggerEvent($"PlayerEarnScore", scoreValue);
            EventManager.TriggerEvent($"PlayerEarnGold", coinValue);
        }

        public float CurrentHealth()
        {
            return currentHealth;
        }

        public float PercentageHealth()
        {
            return currentHealth / startingHealth;
        }
    }
}