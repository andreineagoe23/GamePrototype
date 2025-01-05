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

    private WaveSpawner waveSpawner;

    [Header("Experience Orb")]
    public GameObject experienceOrbPrefab;
    public int orbsToDrop = 3;

    private float fixedHeight; // Variable to store the fixed height

    protected virtual void Start()
    {
        mAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        waveSpawner = FindObjectOfType<WaveSpawner>();

        currentHealth = maxHealth;
        lastAttackTime = -attackCooldown;

        mAnimator.SetTrigger("TrIdle");

        // Set the fixed height to the enemy's current y-position
        fixedHeight = transform.position.y;
    }

    void Update()
    {
        transform.LookAt(player);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > minDistanceToPlayer)
        {
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
        // Move horizontally towards the player
        Vector3 direction = transform.forward * moveSpeed * Time.deltaTime;

        // Maintain fixed height
        transform.position = new Vector3(
            transform.position.x + direction.x,
            fixedHeight,
            transform.position.z + direction.z
        );
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

        DropExperienceOrbs();
        Destroy(gameObject);
    }

    private void DropExperienceOrbs()
    {
        if (experienceOrbPrefab == null) return;

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
