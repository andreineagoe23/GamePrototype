using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;
    public GameObject experienceOrbPrefab;
    public int numberOfOrbs;

    public int experienceValue = 10; 

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took " + amount + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        // Drop experience orbs when the enemy dies
        for (int i = 0; i < numberOfOrbs; i++)
        {
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
        }

        // Notify the player of experience gained (we’ll set this up in ExperienceOrb script)
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