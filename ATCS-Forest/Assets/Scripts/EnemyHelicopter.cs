using UnityEngine;

public class EnemyHelicopter : MonoBehaviour
{
    public delegate void EnemyDeathHandler();
    public event EnemyDeathHandler OnEnemyDeath;

    public float speed = 2f;
    public float stopDistance = 5f;
    public Transform enemyShootPoint;
    public GameObject projectilePrefab;
    public float shootCooldown = 1.5f;
    public int maxHealth = 3;

    private Transform player;
    private Animator animator;
    private float shootTimer;
    private int health;

    private Vector2 noiseOffset;

    // Invincibility
    private bool isInvincible = false;
    private float invincibilityDuration = 0.4f;  // 1 second invincibility
    private float invincibilityTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        health = maxHealth;

        noiseOffset = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));

        if (enemyShootPoint == null)
        {
            enemyShootPoint = transform.Find("EnemyShootPoint");
            if (enemyShootPoint == null)
            {
                Debug.LogError("EnemyShootPoint not assigned and not found as child!");
            }
        }

        shootTimer = shootCooldown;
    }

    void Update()
    {
        // Handle invincibility timer countdown
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                // Optional: reset visual cue here (e.g. color)
            }
        }

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null || enemyShootPoint == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        Patrol();

        if (distance <= stopDistance)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootCooldown;
            }
        }

        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Patrol()
    {
        float driftSpeed = 0.5f;
        float driftMagnitude = 1.5f;

        float x = Mathf.PerlinNoise(Time.time * driftSpeed + noiseOffset.x, 0f) - 0.5f;
        float y = Mathf.PerlinNoise(0f, Time.time * driftSpeed + noiseOffset.y) - 0.5f;

        Vector3 offset = new Vector3(x, y, 0f) * driftMagnitude;
        transform.position += offset * Time.deltaTime;
    }

    void Shoot()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab not assigned!");
            return;
        }

        GameObject bullet = Instantiate(projectilePrefab, enemyShootPoint.position, Quaternion.identity);

        Vector2 direction = (player.position - enemyShootPoint.position).normalized;
        bullet.transform.right = direction;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float bulletSpeed = 10f;
            rb.linearVelocity = direction * bulletSpeed;
        }

        ProjectileNew proj = bullet.GetComponent<ProjectileNew>();
        if (proj != null)
        {
            proj.isEnemyBullet = true;
        }

        Debug.Log("Enemy shooting projectile at time: " + Time.time);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            Debug.Log($"{gameObject.name} is currently invincible. Ignoring damage.");
            return;
        }

        health -= damage;
        Debug.LogWarning($"{gameObject.name} took {damage} damage. Current health: {health}");

        if (health <= 0)
        {
            Debug.Log($"{gameObject.name} health is 0 or less. Dying now.");
            Die();
            return;
        }

        // Start invincibility
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

        // Optional: add visual feedback here for invincibility (e.g. flash color)
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);
    }
}
