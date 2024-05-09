using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public class EnemyHealth : MonoBehaviour, IDamageAble
    {
        public float startingHealth = 100;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public AudioClip deathClip;

        float currentHealth;
        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        EnemyMovement enemyMovement;
        List<GameObject> orbPrefabs = new List<GameObject>();
        public float orbDropChance = 0.5f;


        void Awake ()
        {
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
            enemyMovement = this.GetComponent<EnemyMovement>();
            
            orbPrefabs.Add(Resources.Load("HealOrb") as GameObject);
            orbPrefabs.Add(Resources.Load("AttackOrb") as GameObject);
            orbPrefabs.Add(Resources.Load("SpeedOrb") as GameObject);
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

        void Update ()
        {
            if (IsDead())
            {
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
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

        public void TakeDamage (float amount, Vector3 hitPoint)
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

        void Death ()
        {
            EventManager.TriggerEvent("Sound", this.transform.position);
            anim.SetTrigger ("Dead");
            Vector3 orbSpawnPosition = transform.position;
            orbSpawnPosition.y += 0.5f;

            float orbDropValue = Random.value;
            if (orbDropValue <= orbDropChance) {
                int orbIndex = Random.Range(0, orbPrefabs.Count);
                GameObject orb = orbPrefabs[orbIndex];
                Instantiate(orb, orbSpawnPosition, Quaternion.identity);
            }

            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }

        public void StartSinking ()
        {
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
            SetKinematics(true);

            ScoreManager.score += scoreValue;
        }

        public float CurrentHealth()
        {
            return currentHealth;
        }
    }
}