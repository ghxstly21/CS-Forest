using UnityEngine;

public class FinalSenBoss : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 200;
    private int currentHealth;

    [Header("Enemy Prefabs")]
    public GameObject helicopterPrefab;
    public GameObject miguelPrefab;
    public GameObject aishaPrefab;

    [Header("Spawn Settings")]
    public float spawnCooldown = 5f;
    private float spawnTimer;

    [Header("Spawn Positions (Relative to Boss)")]
    public Vector3 helicopterOffset = new Vector3(0f, 7f, 0f); // High in the sky
    public Vector3 miguelOffset = new Vector3(2f, 0f, 0f);     // Front right
    public Vector3 aishaOffset = new Vector3(-2f, 0f, 0f);      // Back left

    private void Start()
    {
        currentHealth = maxHealth;
        spawnTimer = spawnCooldown;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnEnemies();
            spawnTimer = spawnCooldown;
        }
    }

    void SpawnEnemies()
    {
        if (helicopterPrefab != null)
        {
            Vector3 heliPos = transform.position + helicopterOffset;
            Instantiate(helicopterPrefab, heliPos, Quaternion.identity);
        }

        if (miguelPrefab != null)
        {
            Vector3 miguelPos = transform.position + miguelOffset;
            Instantiate(miguelPrefab, miguelPos, Quaternion.identity);
        }

        if (aishaPrefab != null)
        {
            Vector3 aishaPos = transform.position + aishaOffset;
            Instantiate(aishaPrefab, aishaPos, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Final Sen Boss took {damage} damage, HP left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Final Sen Boss died!");

        Destroy(gameObject);
    }

}
