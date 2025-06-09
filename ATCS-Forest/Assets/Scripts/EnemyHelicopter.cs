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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        health = maxHealth;

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

        if (animator != null)
            animator.SetBool("isShooting", distance <= stopDistance);
    }

    void Patrol()
    {
        float patrolHeight = 2f;
        float patrolSpeed = 2f;
        Vector3 pos = transform.position;
        pos.y += Mathf.Sin(Time.time * patrolSpeed) * Time.deltaTime * patrolHeight;
        transform.position = pos;
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
        health -= damage;
        Debug.Log($"Enemy took {damage} damage, health now {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);



    }
}
