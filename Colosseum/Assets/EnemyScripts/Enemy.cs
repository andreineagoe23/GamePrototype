using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > minDistanceToPlayer)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Attack();
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position += (player.position - transform.position).normalized * moveSpeed * Time.deltaTime;
    }

    public virtual void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy attacks the player!");
            }

            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
