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

    private WaveSpawner waveSpawner; // Reference to the wave spawner

    protected virtual void Start() // Marked as virtual for derived classes
    {
        mAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        waveSpawner = FindObjectOfType<WaveSpawner>();

        currentHealth = maxHealth;
        lastAttackTime = -attackCooldown; // Allows immediate attack if in range
        mAnimator.SetTrigger("TrIdle");
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
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
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

        Destroy(gameObject);
    }
}
