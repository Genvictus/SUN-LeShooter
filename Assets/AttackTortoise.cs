using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Nightmare
{
    public class AttackTortoise : MonoBehaviour
    {
        public NavMeshAgent pet;
        //public GameObject player;

        GameObject[] enemies;
        GameObject target;
        public float attackDist;
        public int damage;
        public float hitTime;
        float _timer;
        // Start is called before the first frame update
        void Start()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            _timer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // Debug.Log(enemies.Length);
            if (enemies.Length == 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player"); 
                pet.SetDestination(player.transform.position);
            }
            else
            {
                var distance = float.MaxValue;
                foreach (var enemy in enemies)
                {
                    float enemyDist = Vector3.Distance(transform.position, enemy.transform.position);
                    if (enemyDist < distance)
                    {
                        distance = enemyDist;
                        target = enemy;
                    }
                }
                // Debug.Log(distance);
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
            EnemyHealth health = target.GetComponent<EnemyHealth>();
            health.TakeDamage(damage, transform.position);
            _timer = 0;
        }
    }
}

