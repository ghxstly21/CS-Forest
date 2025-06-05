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

        
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && player != null)
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
    }
}
