using UnityEngine;
using UnityEngine.AI; // For navigation

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent; // Reference to NavMeshAgent component
    public Transform player; // Reference to player's transform
    public float attackRange = 2f; // Distance to attack the player
    public float attackCooldown = 1f; // Time between attacks
    public int damage = 1; // Damage to player

    private float attackTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        attackTimer = attackCooldown;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Move towards the player
        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            // Attack the player if within range
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            attackTimer = attackCooldown;
        }
    }
}
