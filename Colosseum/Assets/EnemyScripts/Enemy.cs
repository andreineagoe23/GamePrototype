using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator mAnimator;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float minDistanceToPlayer = 5f;
    public float maxDistanceToPlayer = 10f;

    [Header("Attack")]
    public int attackDamage = 10;
    public float attackCooldown = 2f;

    public Transform player;          
    public WaveSpawner waveSpawner;
    private float lastAttackTime;      // Tracks the time of the last attack

    [Header("Health")]
    public int maxHealth;
    int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        mAnimator = GetComponent<Animator>();

        // Initialize references
        player = GameObject.FindGameObjectWithTag("Player").transform;
        waveSpawner = FindObjectOfType<WaveSpawner>();

        // Initialize attack cooldown
        lastAttackTime = -attackCooldown; // Allows immediate attack if in range
    }

    private void Update()
    {
        if (player == null) return;
        
        // Rotate to face the player
        transform.LookAt(player);

        // Move towards the player if out of attack range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > minDistanceToPlayer)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer <= minDistanceToPlayer)
        {
            TryAttackPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        mAnimator.SetTrigger("TrMove");

        // Move forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void TryAttackPlayer()
    {
        // Check if enough time has passed since the last attack
        if (Time.time > lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time; // Reset cooldown timer
        }
    }

    private void AttackPlayer()
    {
        mAnimator.SetTrigger("TrAttack");

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
        Debug.Log($"Enemy took {damage} damage. Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        mAnimator.SetTrigger("TrDie");

        Debug.Log("Enemy died!");

        // Notify the wave spawner that an enemy has been killed
        waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;

        // Destroy the enemy object
        Destroy(gameObject);
    }
}