using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform Player;
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;
    public int attackDamage = 10; // Damage dealt to the player
    public float attackCooldown = 2f; // Time between attacks
    private float lastAttackTime; // Tracks when the enemy last attacked the player

    void Start()
    {
        lastAttackTime = -attackCooldown; // So the enemy can attack immediately if in range
    }

    void Update()
    {
        // Make the enemy face the player
        transform.LookAt(Player);

        // If the enemy is further than MinDist, it moves towards the player
        if (Vector3.Distance(transform.position, Player.position) > MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        // If the enemy is within MinDist, it attacks the player
        else
        {
            // Attack the player if the enemy is within MinDist and the cooldown has passed
            if (Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time; // Reset the attack timer
            }
        }
    }

    void AttackPlayer()
    {
        // Assuming the Player has a script called PlayerHealth to manage its health
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Player took " + attackDamage + " damage!");
        }
    }
}
