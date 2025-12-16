using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;   // Bell Power Up
    public float spawnTime = 3f;

    public Vector2 minPos;
    public Vector2 maxPos;

    public int maxPowerUps = 50;        // N: max allowed on map at once
    public string powerUpTag = "PowerUp"; // tag on the prefab

    void Start()
    {
        InvokeRepeating(nameof(SpawnPowerUp), 1f, spawnTime);
    }

    void SpawnPowerUp()
    {
        // count existing powerups
        int currentCount = GameObject.FindGameObjectsWithTag(powerUpTag).Length;
        if (currentCount >= maxPowerUps)
            return; // already at cap, do not spawn

        Vector2 randomPos = new Vector2(
            Random.Range(minPos.x, maxPos.x),
            Random.Range(minPos.y, maxPos.y)
        );

        Instantiate(powerUpPrefab, randomPos, Quaternion.identity);
    }
}
