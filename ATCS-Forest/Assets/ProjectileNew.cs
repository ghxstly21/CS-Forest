using UnityEngine;

public class ProjectileNew : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 5f;
    public bool isEnemyBullet = false;  

    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (!isEnemyBullet && collision.CompareTag("Enemy"))
        {
            EnemyHelicopter enemy = collision.GetComponent<EnemyHelicopter>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Debug.Log($"Player's bullet hit enemy and dealt {damage} damage.");
            hasHit = true;
            Destroy(gameObject);
        }
        else if (isEnemyBullet && collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Debug.Log($"Enemy's bullet hit player and dealt {damage} damage.");
            hasHit = true;
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Enemy") && !collision.CompareTag("Player"))
        {
            Debug.Log($"Projectile hit {collision.name} and destroyed.");
            hasHit = true;
            Destroy(gameObject);
        }
    }
}
