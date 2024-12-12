using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Reference to the enemy prefab
    public Transform spawnPoint1;   // First spawn point
    public Transform spawnPoint2;   // Second spawn point

    public float initialDelay = 20f;  // Time before spawning starts
    public float spawnInterval = 10f; // Time between alternate spawns

    void Start()
    {
        // Start the spawning coroutine with a delay
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    IEnumerator SpawnEnemiesWithDelay()
    {
        // Wait for the initial delay before starting spawning
        yield return new WaitForSeconds(initialDelay);

        bool spawnAtFirstPoint = true;

        while (true) // Infinite loop to keep spawning enemies alternately
        {
            // Alternate between the two spawn points
            if (spawnAtFirstPoint)
            {
                Instantiate(enemyPrefab, spawnPoint1.position, spawnPoint1.rotation);
            }
            else
            {
                Instantiate(enemyPrefab, spawnPoint2.position, spawnPoint2.rotation);
            }

            // Switch the spawn point for the next enemy
            spawnAtFirstPoint = !spawnAtFirstPoint;

            // Wait for the spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
