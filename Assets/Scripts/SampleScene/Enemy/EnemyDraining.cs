using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDraining : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float sightRange;
    public float drainRange;
    public float drainRate;
    private bool playerInSightRange, playerInAttackRange;
    float attackTimer;

    public float damage = 10;

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
        playerInAttackRange = Physics.CheckSphere(transform.position, drainRange, whatIsPlayer);

        attackTimer += Time.deltaTime;

        if (playerInSightRange && playerInAttackRange)
        {
            if (attackTimer >= drainRate)
                AttackPlayer();
            
        }
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        IMobs enemyHealth = player.gameObject.GetComponent<IMobs>();
        enemyHealth?.TakeDamage(damage);

        Debug.DrawLine(transform.position, transform.forward * drainRange, Color.red, 2f);
        attackTimer = 0;

        // Vector3 bulletSpawnPostion = transform.position + transform.forward * 1.5f;

        // Rigidbody rb = Instantiate(projectile, bulletSpawnPostion, Quaternion.identity).GetComponent<Rigidbody>();
        // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        // rb.AddForce(transform.up * 4f, ForceMode.Impulse);
        // attackTimer = timeBetweenAttacks;

        // Destroy(rb.gameObject, 2f);

        // Debug.Log("Enemy has shot");
    }
}
