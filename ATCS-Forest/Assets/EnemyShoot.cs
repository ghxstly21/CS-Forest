using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform EnemyShootPoint;
    public float shootCooldown = 0.5f;
    public float bulletSpeed = 10f;

    private float cooldownTimer = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (EnemyShootPoint == null)
        {
            EnemyShootPoint = transform.Find("EnemyShootPoint");
            if (EnemyShootPoint == null)
            {
                Debug.LogError("EnemyShootPoint not assigned and not found as a child!");
            }
        }
    }

    void Update()
    {
        if (player == null || EnemyShootPoint == null) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, EnemyShootPoint.position, Quaternion.identity);

        Vector2 direction = (player.position - EnemyShootPoint.position).normalized;

        bullet.transform.right = direction;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Projectile prefab is missing a Rigidbody2D!");
        }
    }
}
