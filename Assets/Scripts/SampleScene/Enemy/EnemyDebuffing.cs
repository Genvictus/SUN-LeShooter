using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDebuffing : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float sightRange;
    public float debuffRange;
    public float debuffStrength = 0.5f;
    private bool playerInSightRange, playerInAttackRange;
    private PlayerMovement playerMovement;
    public Gun Default;
    public Gun Shotgun;
    public Melee Sword;

    bool isDebuffed = false;

    protected void Awake()
    {
        player = GameObject.Find("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start() {
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, debuffRange, whatIsPlayer);

        if (playerInSightRange && playerInAttackRange)
        {
            if (!isDebuffed)
                Debuff();
            
        } else if (!playerInAttackRange)
        {
            if (isDebuffed)
                Rebuff();
        }
    }

    void Debuff()
    {
        isDebuffed = true;
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        playerMovement.walkSpeed /= debuffStrength;
        playerMovement.runSpeed /= debuffStrength;

        Default.DebuffAttack(debuffStrength);

        Sword.DebuffAttack(debuffStrength);

        Shotgun.DebuffAttack(debuffStrength);
        // Vector3 bulletSpawnPostion = transform.position + transform.forward * 1.5f;

        // Rigidbody rb = Instantiate(projectile, bulletSpawnPostion, Quaternion.identity).GetComponent<Rigidbody>();
        // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        // rb.AddForce(transform.up * 4f, ForceMode.Impulse);
        // attackTimer = timeBetweenAttacks;

        // Destroy(rb.gameObject, 2f);

        // Debug.Log("Enemy has shot");
    }

      void Rebuff()
    {
        isDebuffed = false;
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        playerMovement.walkSpeed *= debuffStrength;
        playerMovement.runSpeed *= debuffStrength;

        Default.BuffAttack(debuffStrength);

        Sword.BuffAttack(debuffStrength);

        Shotgun.BuffAttack(debuffStrength);

        // Vector3 bulletSpawnPostion = transform.position + transform.forward * 1.5f;

        // Rigidbody rb = Instantiate(projectile, bulletSpawnPostion, Quaternion.identity).GetComponent<Rigidbody>();
        // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        // rb.AddForce(transform.up * 4f, ForceMode.Impulse);
        // attackTimer = timeBetweenAttacks;

        // Destroy(rb.gameObject, 2f);

        // Debug.Log("Enemy has shot");
    }
}
