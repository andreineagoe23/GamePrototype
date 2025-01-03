using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator mAnimator;

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

    protected virtual void Start() // Marked as virtual for derived classes
    {
<<<<<<< HEAD
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        lastAttackTime = -attackCooldown;
=======
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

        mAnimator.SetTrigger("TrIdle");
>>>>>>> origin/EnemySpawns
    }

    void Update()
    {
<<<<<<< HEAD
        if (player == null) return;

=======
        // Rotate to face the player
        transform.LookAt(player);

        // Calculate the distance to the player
>>>>>>> origin/EnemySpawns
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > minDistanceToPlayer)
        {
            // Only trigger "TrMove" if the current animation is not already "Move"
            if (!mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            {
                mAnimator.SetTrigger("TrMove");
            }

            MoveTowardsPlayer();
        }
        else
        {
            Attack();
        }
    }

    private void MoveTowardsPlayer()
    {
<<<<<<< HEAD
        transform.position += (player.position - transform.position).normalized * moveSpeed * Time.deltaTime;
=======

        // Move forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
>>>>>>> origin/EnemySpawns
    }

    public virtual void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
<<<<<<< HEAD
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy attacks the player!");
            }

            lastAttackTime = Time.time;
=======
            mAnimator.SetTrigger("TrAttack");
            AttackPlayer();
            lastAttackTime = Time.time; // Reset cooldown timer
        }
    }

    private void AttackPlayer()
    {
        

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Player took " + attackDamage + " damage!");
>>>>>>> origin/EnemySpawns
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
<<<<<<< HEAD
=======

        Debug.Log("Enemy died!");

        // Notify the wave spawner that an enemy has been killed
        waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;

        // Destroy the enemy object
>>>>>>> origin/EnemySpawns
        Destroy(gameObject);
    }
}
