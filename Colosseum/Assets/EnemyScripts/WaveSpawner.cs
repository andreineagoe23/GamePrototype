using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public Wave[] waves;  // Array to hold different waves of enemies
    public Transform[] spawnPoints;  // Multiple spawn points for enemies

    public float timeBetweenWaves = 5f;  // Time between waves
    private int currentWaveIndex = 0;
    private float countdown;

    void Start()
    {
        countdown = timeBetweenWaves;
    }

    void Update()
    {
        // If all waves are completed, exit
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves completed!");
            return;
        }

        // If the countdown reaches zero, spawn the next wave
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

    // Coroutine to spawn each wave
    private IEnumerator SpawnWave()
    {
        Wave wave = waves[currentWaveIndex];
        Debug.Log($"Spawning Wave {currentWaveIndex + 1}");

        // Loop through each enemy type in the wave
        foreach (EnemyData enemyData in wave.enemyData)
        {
            // Spawn the specified number of enemies for the current enemy type
            for (int i = 0; i < enemyData.enemyCount; i++)
            {
                SpawnEnemy(enemyData.enemyPrefab);
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }

        currentWaveIndex++;  // Move to the next wave
    }

    // Spawn an enemy at a random spawn point
    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Select a random spawn point from the available spawn points
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

// Define EnemyData before the Wave class to avoid the error
[System.Serializable]
public class EnemyData
{
    public GameObject enemyPrefab;   // Reference to the enemy prefab
    public int enemyCount;           // Number of enemies to spawn for this type
}

[System.Serializable]
public class Wave
{
    [Header("Wave Configuration")]
    public EnemyData[] enemyData;  // Array to store data about each enemy in the wave
    public float spawnInterval;    // Time between spawning enemies in the wave
}
