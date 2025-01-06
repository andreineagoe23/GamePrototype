using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public float moveSpeed = 5f; // Base speed at which the orb moves toward the player
    public float maxSpeed = 20f; // Maximum speed as the orb gets closer
    public float acceleration = 10f; // Acceleration factor for smooth speed increase
    private Transform player; // Reference to the player
    private bool isCollected = false; // To check if the orb has been collected
    public float absorptionDistance = 0.5f; // Distance at which the orb is "absorbed"

    private void Start()
    {
        // Find the player by tag (ensure player tag is "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Move the orb toward the player if it hasn't been collected
        if (!isCollected)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Gradually increase the speed as the orb gets closer
        moveSpeed = Mathf.Min(moveSpeed + acceleration * Time.deltaTime, maxSpeed);

        // Move the orb toward the player
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);

        // If the orb is within the absorption distance, collect it
        if (Vector3.Distance(transform.position, player.position) < absorptionDistance)
        {
            CollectOrb();
        }
    }

    private void CollectOrb()
    {
        if (!isCollected)
        {
            // Add experience points to the player
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.AddExperience(playerController.xpPerOrb);
            }

            // Play a collection sound or effect here
            Debug.Log("Experience orb collected!");

            // Destroy the orb (collected)
            Destroy(gameObject);
            isCollected = true;
        }
    }
}
