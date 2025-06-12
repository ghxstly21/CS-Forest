using UnityEngine;

public class MiguelEnemySimple : MonoBehaviour
{
    public float patrolSpeed = 1.5f;
    public float patrolRange = 3f;
    public Transform enemyShootPoint;
    public GameObject projectilePrefab;
    public float shootCooldown = 2f;
    public int maxHealth = 3;
    public int xpDrop = 30;

    private int health;
    private float shootTimer;
    private Vector3 startPosition;
    private Animator animator;

    private float lastPosX;

    private bool isInvincible = false;
    private float invincibilityDuration = 0.3f;
    private float invincibilityTimer = 0f;

    void Start()
    {
        health = maxHealth;
        shootTimer = shootCooldown;
        startPosition = transform.position;
        animator = GetComponent<Animator>();

        if (enemyShootPoint == null)
        {
            enemyShootPoint = transform.Find("EnemyShootPoint");
            if (enemyShootPoint == null)
            {
                Debug.LogError("EnemyShootPoint not assigned or found!");
            }
        }

        lastPosX = transform.position.x;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
                isInvincible = false;
        }

        Patrol();

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootRandom();
            shootTimer = shootCooldown;
        }

        // Flip and animation
        float currentPosX = transform.position.x;
        float deltaX = currentPosX - lastPosX;

        if (Mathf.Abs(deltaX) > 0.01f)
        {
            transform.localScale = new Vector3(deltaX > 0 ? 2 : -2, 2, 2);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        lastPosX = currentPosX;
    }

    void Patrol()
    {
        float patrolX = startPosition.x + Mathf.PingPong(Time.time * patrolSpeed, patrolRange * 2) - patrolRange;
        transform.position = new Vector3(patrolX, transform.position.y, transform.position.z);
    }

    void ShootRandom()
    {
        if (projectilePrefab == null || enemyShootPoint == null) return;

        GameObject bullet = Instantiate(projectilePrefab, enemyShootPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float horizontalDir = Random.value < 0.5f ? -1f : 1f;
            float verticalDir = Random.Range(-0.3f, 0.3f);
            Vector2 direction = new Vector2(horizontalDir, verticalDir).normalized;
            rb.linearVelocity = direction * 8f;
            bullet.transform.right = direction;
        }

        ProjectileNew proj = bullet.GetComponent<ProjectileNew>();
        if (proj != null)
        {
            proj.isEnemyBullet = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
        }
    }

    void Die()
    {
        PlayerXP playerXP = FindObjectOfType<PlayerXP>();
        if (playerXP != null)
        {
            playerXP.GainXP(xpDrop);
        }

        Destroy(gameObject);
    }
}
