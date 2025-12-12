using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount = 5;
        public float spawnDelay = 0.5f;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    int currentWave = 0;
    int enemiesSpawned = 0;
    float spawnTimer = 0f;
    bool waveActive = false;

    void Start()
    {
        StartWave(currentWave);
    }

    void Update()
    {
        if (!waveActive) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWave].spawnDelay &&
            enemiesSpawned < waves[currentWave].enemyCount)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }

        if (enemiesSpawned >= waves[currentWave].enemyCount &&
            GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            NextWave();
        }
    }

    void SpawnEnemy()
    {
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
        enemiesSpawned++;
    }

    void StartWave(int index)
    {
        if (index >= waves.Length) return;
        enemiesSpawned = 0;
        spawnTimer = 0f;
        waveActive = true;
    }

    void NextWave()
    {
        waveActive = false;
        currentWave++;

        if (currentWave < waves.Length)
        {
            StartWave(currentWave);
        }
        else
        {
            Debug.Log("ALL WAVES COMPLETED!");
        }
    }
}
