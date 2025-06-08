using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public TMP_Text enemiesLeftText;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int enemiesPerWave = 10;
    public float spawnInterval = 3f;

    private int enemiesSpawned = 0;
    private int enemiesAlive = 0;
    private float spawnTimer;

    public string nextSceneName;

    void Start()
    {
        spawnTimer = spawnInterval;
        UpdateEnemiesLeftText();
    }

    void Update()
    {
        if (enemiesSpawned < enemiesPerWave)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval;
            }
        }
        else if (enemiesAlive == 0)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        UpdateEnemiesLeftText();
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);

        enemiesSpawned++;
        enemiesAlive++;

        EnemyHelicopter enemyScript = enemy.GetComponent<EnemyHelicopter>();
        if (enemyScript != null)
        {
            enemyScript.OnEnemyDeath += EnemyDied;
        }
    }

    void EnemyDied()
    {
        enemiesAlive--;
        UpdateEnemiesLeftText();
    }

    void UpdateEnemiesLeftText()
    {
        enemiesLeftText.text = $"Enemies Left: {enemiesAlive}";
    }
}
