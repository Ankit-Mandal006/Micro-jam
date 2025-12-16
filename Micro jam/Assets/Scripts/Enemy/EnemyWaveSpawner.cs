using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyEntry
    {
        public GameObject enemyPrefab;  // which enemy (Enemy1, Enemy2, etc.)
        public int count = 5;           // how many of this type
    }

    [System.Serializable]
    public class Wave
    {
        public float spawnDelay = 0.5f;
        public EnemyEntry[] enemies;    // list of enemy types in this wave
    }

    public GameObject WiningScreen;
    public Wave[] waves;
    public Transform[] spawnPoints;

    int currentWave = 0;
    int currentEntryIndex = 0;
    int spawnedInCurrentEntry = 0;

    float spawnTimer = 0f;
    bool waveActive = false;

    void Start()
    {
        StartWave(0);
    }

    void Update()
    {
        if (!waveActive) return;

        spawnTimer += Time.deltaTime;

        Wave wave = waves[currentWave];

        // still have enemies left to spawn in this wave?
        if (currentEntryIndex < wave.enemies.Length &&
            spawnTimer >= wave.spawnDelay)
        {
            spawnTimer = 0f;
            SpawnEnemyFromCurrentEntry();
        }

        // when all spawned AND no enemies alive → next wave
        if (currentEntryIndex >= wave.enemies.Length &&
            GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            NextWave();
        }
    }

    void SpawnEnemyFromCurrentEntry()
    {
        Wave wave = waves[currentWave];
        EnemyEntry entry = wave.enemies[currentEntryIndex];

        if (entry.enemyPrefab == null) return;

        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(entry.enemyPrefab, randomPoint.position, Quaternion.identity);

        spawnedInCurrentEntry++;

        // move to next enemy type in this wave when done with this one
        if (spawnedInCurrentEntry >= entry.count)
        {
            spawnedInCurrentEntry = 0;
            currentEntryIndex++;
        }
    }

    void StartWave(int index)
    {
        if (index >= waves.Length) return;

        currentWave = index;
        currentEntryIndex = 0;
        spawnedInCurrentEntry = 0;
        spawnTimer = 0f;
        waveActive = true;
    }

    void NextWave()
    {
        waveActive = false;
        int next = currentWave + 1;

        if (next < waves.Length)
        {
            StartWave(next);
        }
        else
        {
            WiningScreen.SetActive(true);
            Time.timeScale=0f;
            Debug.Log("ALL WAVES COMPLETED!");
        }
    }
}
