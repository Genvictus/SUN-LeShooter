using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Nightmare
{
    public class IncreaseTortoise : PausibleObject
    {
        // Start is called before the first frame update
        public NavMeshAgent pet;
        GameObject player;
        public GameObject followTarget;
        public EnemyHealth followTargetHealth;
        EnemyPetHealth petHealth;
        float scale = 0.1f;


        void Start()
        {
            followTargetHealth = followTarget.GetComponent<EnemyHealth>();
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            pet.speed = 0.75f * playerMovement.speed;
            petHealth = GetComponent<EnemyPetHealth>();
        }


        void SetFollowTargetHealth()
        {
            followTargetHealth = followTarget.GetComponent<EnemyHealth>();
        }

        // Update is called once per frame
        void Update()
        {
            SetFollowTargetHealth();
            if (followTargetHealth.IsDead())
            {
                petHealth.Death();
            }

            if (isPaused || petHealth.isDead)
                return;


            float playerDist = Vector3.Distance(transform.position, player.transform.position);
            if(playerDist <= 6f)
            {
                EscapePlayer();
            }
            else
            {
                pet.SetDestination(followTarget.transform.position);
                /*List<GameObject> enemyBosses = new List<GameObject>();
                foreach (var enemy in enemies)
                {
                    if (enemy.name == "Jenderal" || enemy.name == "Raja" || enemy.name == "Jenderal(Clone)" || enemy.name == "Raja(Clone)")
                    {
                        enemyBosses.Add(enemy);
                    }
                }

                Debug.Log("bos count: " + enemyBosses.Count);

                if (enemyBosses.Count > 0)
                {
                    float dist = float.MaxValue;

                    foreach(var bos in enemyBosses)
                    {
                        float temp = Vector3.Distance(transform.position, bos.transform.position);
                        if(temp < dist)
                        {
                            dist = temp;
                            followTarget = bos;
                        }
                    }

                    pet.SetDestination(followTarget.transform.position);
                }*/
                /*else
                {
                    return;
                }*/
            }
            
        }

        void EscapePlayer()
        {
            Debug.Log("Escaping player");
            float gradient = (transform.position.x - player.transform.position.x) - (transform.position.z - player.transform.position.z);

            float y = transform.position.y;
            float x = transform.position.x + scale * gradient;
            float z = transform.position.y + scale * gradient;
            Vector3 targetPosition = new Vector3(x, y, z);

            pet.SetDestination(targetPosition);
        }

        /*public void TakeDamage(float damage, Vector3 hitPoint)
        {
            throw new System.NotImplementedException();
        }*/
    }
}

