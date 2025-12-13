using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoPickupPrefab;
    public int initialSpawnCount = 10;

    
    public Vector2 minPos = new Vector2(-10f, -5f);
    public Vector2 maxPos = new Vector2( 10f,  5f);

    public Transform player;          
    public float minDistanceFromPlayer = 3f;
    public float protectDuration = 10f;   

    void Start()
    {
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        if (ammoPickupPrefab == null) return;

        Vector3 pos = GetRandomPositionFarFromPlayer();
        Instantiate(ammoPickupPrefab, pos, Quaternion.identity);
    }

    Vector3 GetRandomPositionFarFromPlayer()
    {
        Vector3 pos = Vector3.zero;
        int safety = 0;

        bool useDistanceRule = Time.time >= protectDuration;   

        do
        {
            float x = Random.Range(minPos.x, maxPos.x);
            float y = Random.Range(minPos.y, maxPos.y);
            pos = new Vector3(x, y, 0f);

            safety++;
            if (safety > 50) break;   
        }
        while (useDistanceRule &&
               player != null &&
               Vector3.Distance(pos, player.position) < minDistanceFromPlayer);

        return pos;
    }
}
