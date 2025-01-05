using UnityEngine;
using UnityEngine.AI;  // Ensure to include this

public class Enemy : MonoBehaviour
{
    private Animator mAnimator;
    private NavMeshAgent agent;  // Reference to the NavMesh Agent

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float minDistanceToPlayer = 5f;

    [Header("Attack")]
    public int attackDamage = 10;
    public float attackCooldown = 2f;

    public Transform player;
    private float lastAttackTime;

    [Header("Health")]
    public int maxHealth;
    private int currentHealth;

    private float fixedHeight;

    [Header("Experience Orb")]
    public GameObject experienceOrbPrefab;  // Reference to the experience orb prefab
    public int orbsToDrop = 3;  // Number of orbs to drop on death

    protected virtual void Start()
    {
        mAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();  // Get the NavMesh Agent component

        currentHealth = maxHealth;
        lastAttackTime = -attackCooldown;

        mAnimator.SetTrigger("TrIdle");

        // Set the fixed height to the enemy's current y-position
        fixedHeight = transform.position.y;
    }

    void Update()
    {
        // Get the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > minDistanceToPlayer)
        {
            if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            {
                mAnimator.SetTrigger("TrMove");
            }
            MoveTowardsPlayer();  // Move towards the player using NavMesh
        }
        else
        {
            Attack();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Make the enemy move towards the player using NavMesh Agent
        agent.SetDestination(player.position);
        agent.speed = moveSpeed;  // Set the speed of the agent
    }

    public virtual void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            mAnimator.SetTrigger("TrAttack");
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Player took " + attackDamage + " damage!");
        }
    }

    public void TakeDamage(int damage)
    {
        mAnimator.SetTrigger("TrDamage");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            mAnimator.SetTrigger("TrDie");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");

        // Drop experience orbs
        DropExperienceOrbs();

        Destroy(gameObject);  // Destroy the enemy object when health is 0
    }

    private void DropExperienceOrbs()
    {
        if (experienceOrbPrefab == null) return;  // Ensure there's an orb prefab

        // Drop multiple orbs
        for (int i = 0; i < orbsToDrop; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-1f, 1f),
                0.5f,
                Random.Range(-1f, 1f)
            );

            Instantiate(experienceOrbPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
