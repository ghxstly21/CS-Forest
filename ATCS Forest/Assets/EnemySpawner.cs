using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform player;
    public float spawnDistance = 20f; 
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;

    private float nextSpawnX;

    void Start()
    {
        nextSpawnX = player.position.x + spawnDistance;
    }

    void Update()
    {
        if (player.position.x >= nextSpawnX - spawnDistance)
        {
            SpawnEnemy();
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            nextSpawnX += interval + spawnDistance;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("EnemySpawner: No enemy prefabs assigned!");
            return;
        }

        int index = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = new Vector3(nextSpawnX, player.position.y + Random.Range(-0.5f, 0.5f), 0);
        Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
    }
}
