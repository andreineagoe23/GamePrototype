using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took " + amount + " damage. Current health: " + currentHealth);


        if (currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        // Death function
        // TEMPORARY: Destroy Object
        Debug.Log("Enemy died!");

        Destroy(gameObject);
    }
}

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float moveSpeed = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate direction
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy using Rigidbody (apply force or velocity)
            rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }
}