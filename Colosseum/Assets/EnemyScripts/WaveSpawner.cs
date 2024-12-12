using UnityEngine;
using System.Collections;
public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawnPoint;

    public Wave[] waves;
    public int currentWaveIndex = 0;

    private bool readyToCountDown = true;
    private float currentCountdown;

    void Start()
    {
        currentCountdown = countdown;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("You survived every wave!");
            return;
        }

        if (readyToCountDown == true)
        {
            currentCountdown -= Time.deltaTime;
            if (currentCountdown <= 0)
            {
                readyToCountDown = false;
                StartCoroutine(SpawnWave());
            }
        }

        if (waves[currentWaveIndex].enemiesLeft == 0 && !readyToCountDown)
        {
            readyToCountDown = true;
            currentWaveIndex++;
            currentCountdown = waves[currentWaveIndex].timeToNextWave;
        }
    }

    private IEnumerator SpawnWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                Enemy enemy = Instantiate(currentWave.enemies[i], spawnPoint.transform);

                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}