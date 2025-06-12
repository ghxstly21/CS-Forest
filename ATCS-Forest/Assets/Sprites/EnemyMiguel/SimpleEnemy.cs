using UnityEngine;

public class MiguelEnemySimple : MonoBehaviour
{
    public float patrolSpeed = 1.5f;
    public float patrolRange = 3f;
    public Transform enemyShootPoint;
    public GameObject projectilePrefab;
    public float shootCooldown = 2f;

    private Vector3 startPosition;
    private float shootTimer;
    private Animator animator;

    private float lastPosX;

    void Start()
    {
        startPosition = transform.position;
        shootTimer = shootCooldown;

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
        Patrol();

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootRandom();
            shootTimer = shootCooldown;
        }

        // Check movement direction and flip
        float currentPosX = transform.position.x;
        float deltaX = currentPosX - lastPosX;

        if (deltaX > 0.01f)
        {
            transform.localScale = new Vector3(2, 2, 2);
            animator.SetBool("IsWalking", true);
        }
        else if (deltaX < -0.01f)
        {
            transform.localScale = new Vector3(-2, 2, 2);
            animator.SetBool("IsWalking", true);
        }
        else
        {
        }

        lastPosX = currentPosX;
    }

    void Patrol()
    {
        // Simple left-right patrol in patrolRange around start position
        float patrolX = startPosition.x + Mathf.PingPong(Time.time * patrolSpeed, patrolRange * 2) - patrolRange;
        transform.position = new Vector3(patrolX, transform.position.y, transform.position.z);
    }

    void ShootRandom()
    {
        if (projectilePrefab == null || enemyShootPoint == null)
            return;

        // Shoot projectile in a random direction left or right (for variety)
        GameObject bullet = Instantiate(projectilePrefab, enemyShootPoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Random direction either left or right with some vertical variation
            float horizontalDir = Random.value < 0.5f ? -1f : 1f;
            float verticalDir = Random.Range(-0.3f, 0.3f);

            Vector2 direction = new Vector2(horizontalDir, verticalDir).normalized;
            rb.linearVelocity = direction * 8f;  // Speed of projectile
            bullet.transform.right = direction;
        }
    }
}
