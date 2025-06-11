using UnityEngine;

public class BossHelicopter : MonoBehaviour
{
    public static int cloneCounter = 0;
    public static int maxClones = 9; // Dies on the 10th hit

    public delegate void EnemyDeathHandler();
    public event EnemyDeathHandler OnEnemyDeath;

    [Header("Boss Settings")]
    public float stopDistance = 7f;
    public int maxHealth = 1; // Keep this 1 since we're faking multiple hits

    [Header("References")]
    public Transform enemyShootPoint;
    public GameObject projectilePrefab;
    public GameObject[] miniHelicopterPrefabs;

    [Header("Cooldowns")]
    public float shootCooldown = 0.75f;
    public float spawnCooldown = 5f;

    private Transform player;
    private Animator animator;
    private float shootTimer;
    private float spawnTimer;
    private int currentHealth;
    private Vector2 noiseOffset;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        noiseOffset = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
        shootTimer = shootCooldown;
        spawnTimer = spawnCooldown;

        if (enemyShootPoint == null)
        {
            enemyShootPoint = transform.Find("EnemyShootPoint");
            if (enemyShootPoint == null)
                Debug.LogError("❌ EnemyShootPoint not assigned and not found as child!");
        }
    }

    void Update()
    {
        // Dynamically find player if not found yet
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                Debug.Log("✅ Boss found the player.");
            }
            else
            {
                return; // Wait until player is found
            }
        }

        if (enemyShootPoint == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        Patrol();

        // Shoot if player is within range
        if (distance <= stopDistance)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootCooldown;
            }
        }

        // Spawn mini helicopters periodically
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnMiniHelicopter();
            spawnTimer = spawnCooldown;
        }

        // Flip to face player
        transform.localScale = new Vector3(player.position.x < transform.position.x ? -3 : 3, 3, 3);
    }

    void Patrol()
    {
        float driftSpeed = 0.4f;
        float driftMagnitude = 2f;

        float x = Mathf.PerlinNoise(Time.time * driftSpeed + noiseOffset.x, 0f) - 0.5f;
        float y = Mathf.PerlinNoise(0f, Time.time * driftSpeed + noiseOffset.y) - 0.5f;

        Vector3 offset = new Vector3(x, y, 0f) * driftMagnitude;
        transform.position += offset * Time.deltaTime;
    }

    void Shoot()
    {
        if (projectilePrefab == null) return;

        GameObject bullet = Instantiate(projectilePrefab, enemyShootPoint.position, Quaternion.identity);
        Vector2 direction = (player.position - enemyShootPoint.position).normalized;
        bullet.transform.right = direction;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * 14f;

        ProjectileNew proj = bullet.GetComponent<ProjectileNew>();
        if (proj != null)
            proj.isEnemyBullet = true;

        Debug.Log("🚀 Boss shoots projectile.");
    }

    void SpawnMiniHelicopter()
    {
        if (miniHelicopterPrefabs == null || miniHelicopterPrefabs.Length == 0) return;

        Debug.Log("🚁 Attempting to spawn mini helicopter...");

        int prefabIndex = Random.Range(0, miniHelicopterPrefabs.Length);
        GameObject selectedPrefab = miniHelicopterPrefabs[prefabIndex];

        float offsetDistance = 2.5f;
        float facingDirection = transform.localScale.x > 0 ? 1 : -1;

        Vector3 spawnOffset = new Vector3(facingDirection * offsetDistance, 1f, 0f);
        Vector3 spawnPos = transform.position + spawnOffset;

        GameObject mini = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        OrbitAroundBoss orbit = mini.AddComponent<OrbitAroundBoss>();
        orbit.center = this.transform;
        orbit.radius = offsetDistance;
        orbit.speed = 1f;

        Debug.Log("✅ Mini helicopter spawned and orbiting.");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Boss took {damage} damage, health now {currentHealth}");

        if (currentHealth <= 0)
        {
            if (cloneCounter < maxClones)
            {
                // Clone before destroying to fake survival
                Instantiate(gameObject, transform.position, transform.rotation);
                cloneCounter++;
                Debug.Log($"👻 Clone {cloneCounter} spawned. Boss not dead yet.");
            }
            else
            {
                Debug.Log("💥 Boss actually destroyed!");
                OnEnemyDeath?.Invoke();
            }

            Destroy(gameObject);
        }
    }
}
