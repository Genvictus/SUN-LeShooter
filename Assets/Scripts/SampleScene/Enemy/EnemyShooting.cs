using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public float timeBetweenAttacks;
    float attackTimer;
    public GameObject projectile;

    protected void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        attackTimer = 0;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        attackTimer -= Time.deltaTime;
        if (playerInAttackRange && playerInSightRange && attackTimer <= 0) AttackPlayer();
    }

    void AttackPlayer()
    {
        // agent.SetDestination(transform.position);
        // transform.LookAt(player);

        // Vector3 bulletSpawnPostion = transform.position + transform.forward * 1.5f;

        // Rigidbody rb = Instantiate(projectile, bulletSpawnPostion, Quaternion.identity).GetComponent<Rigidbody>();
        // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        // rb.AddForce(transform.up * 4f, ForceMode.Impulse);
        // attackTimer = timeBetweenAttacks;

        // Destroy(rb.gameObject, 2f);

        // Debug.Log("Enemy has shot");
    }
}
