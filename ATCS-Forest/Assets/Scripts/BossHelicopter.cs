using UnityEngine;

public class BossHelicopter : MonoBehaviour
{
    public delegate void EnemyDeathHandler();
    public event EnemyDeathHandler OnEnemyDeath;

    [Header("Boss Settings")]
    public float stopDistance = 7f;
    public int maxHealth = 10;

    [Header("References")]
    public Transform enemyShootPoint;  // Assign in Inspector or auto-find
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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        noiseOffset = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
        shootTimer = shootCooldown;
        spawnTimer = spawnCooldown;

        if (enemyShootPoint == null)
        {
            enemyShootPoint = transform.Find("EnemyShootPoint");
            if (enemyShootPoint == null)
                Debug.LogError("EnemyShootPoint not assigned and not found as child!");
        }
    }

    void Update()
    {
        if (player == null || enemyShootPoint == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        Patrol();

        // Shoot if player close enough and cooldown elapsed
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

        // Optional animator control - comment out if you don't use animations
        // if (animator != null)
        //     animator.SetBool("isShooting", distance <= stopDistance);
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

        Debug.Log("Boss shoots projectile at: " + Time.time);
    }

    void SpawnMiniHelicopter()
{
    if (miniHelicopterPrefabs == null || miniHelicopterPrefabs.Length == 0) return;

    int prefabIndex = Random.Range(0, miniHelicopterPrefabs.Length);
    GameObject selectedPrefab = miniHelicopterPrefabs[prefabIndex];

    float offsetDistance = 2.5f;
    float facingDirection = transform.localScale.x > 0 ? 1 : -1;

    Vector3 spawnOffset = new Vector3(facingDirection * offsetDistance, 1f, 0f); // in front and slightly above
    Vector3 spawnPos = transform.position + spawnOffset;

    GameObject mini = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

    // Give it a script that orbits around the boss
    OrbitAroundBoss orbit = mini.AddComponent<OrbitAroundBoss>();
    orbit.center = this.transform;
    orbit.radius = offsetDistance;
    orbit.speed = 1f;

    Debug.Log("🚁 Spawned mini helicopter flying around boss.");
}


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Boss took {damage} damage, health now {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss helicopter destroyed!");
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);
    }
} 