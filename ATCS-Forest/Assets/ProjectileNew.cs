using UnityEngine;

public class ProjectileNew : MonoBehaviour
{
    public bool isEnemyBullet = false;
    public int damage = 1;

    private bool hasHit = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (!isEnemyBullet)
        {
            // First check for Boss
            BossHelicopter boss = collision.GetComponent<BossHelicopter>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
                Debug.Log("💥 Hit BOSS for " + damage + " damage.");
                hasHit = true;
                Destroy(gameObject);
                return;
            }

            // Then check for regular enemy
            EnemyHelicopter enemy = collision.GetComponent<EnemyHelicopter>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("💥 Hit ENEMY for " + damage + " damage.");
                hasHit = true;
                Destroy(gameObject);
                return;
            }
        }
        else if (isEnemyBullet && collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponentInParent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("💥 Hit PLAYER for " + damage + " damage.");
            }
            else
            {
                Debug.LogWarning("⚠️ PlayerHealth not found on Player!");
            }

            hasHit = true;
            Destroy(gameObject);
        }

        // If it hits something else like a wall
        if (!hasHit)
        {
            Debug.Log("🧱 Projectile hit " + collision.name + " and was destroyed.");
            hasHit = true;
            Destroy(gameObject);
        }
    }
}
