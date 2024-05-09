using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Nightmare
{
    public class AttackTortoise : PausibleObject
    {
        public NavMeshAgent pet;
        //public GameObject player;

        GameObject[] enemies;
        GameObject target;
        public float attackDist;
        public int damage;
        public float hitTime;
        float _timer;
        Animator animator;
        AudioSource audio;
        GameObject player;

        PetHealth petHealth;
        // Start is called before the first frame update
        void Start()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            player = GameObject.FindGameObjectWithTag("Player");
            petHealth = GetComponent<PetHealth>();
            animator = GetComponent<Animator>();
            audio = GetComponent<AudioSource>();
            _timer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused || petHealth.isDead)
                return;

            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            animator.SetBool("Attack", false);

            List<GameObject> enemiesInRangePlayer = new List<GameObject>();
            foreach (var enemy in enemies)
            {
                float enemyPlayerDist = Vector3.Distance(player.transform.position, enemy.transform.position);
                if (enemyPlayerDist < 15f)
                {
                    enemiesInRangePlayer.Add(enemy);
                }
            }

            if (enemiesInRangePlayer.Count == 0)
            { 
                pet.SetDestination(player.transform.position);
            }
            else
            {
                var distance = float.MaxValue;
                foreach (var enemy in enemiesInRangePlayer)
                {
                    float enemyDist = Vector3.Distance(transform.position, enemy.transform.position);
                    if (enemyDist < distance)
                    {
                        distance = enemyDist;
                        target = enemy;
                    }
                }

                if (distance < attackDist)
                {
                    if (_timer > hitTime)
                    {
                        Attack(target);
                    }
                }

                pet.SetDestination(target.transform.position);
            }
            _timer += Time.deltaTime;
        }
        void Attack(GameObject target)
        {
            audio.Play();
            animator.SetBool("Attack", true);
            StartCoroutine(DamageEnemy());
            _timer = 0;
        }

        IEnumerator DamageEnemy()
        {
            yield return new  WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            EnemyHealth health = target.GetComponent<EnemyHealth>();
            health.TakeDamage(damage, transform.position);
        }

        void OnDestroy()
        {
            StopPausible();
        }
    }
}

