using UnityEngine;

public class ProjectileNew : MonoBehaviour
{
    public bool isEnemyBullet = false;
    public float damage = 1;

    private bool hasHit = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (!isEnemyBullet)
        {
            if (collision.TryGetComponent(out BossHelicopter boss))
            {
                boss.TakeDamage(damage);
                Debug.Log("💥 Hit BOSS for " + damage + " damage.");
            }
            else if (collision.TryGetComponent(out EnemyHelicopter enemy))
            {
                enemy.TakeDamage(damage);
                Debug.Log("💥 Hit ENEMY for " + damage + " damage.");
            }

            hasHit = true;
            Destroy(gameObject);
        }
        else if (isEnemyBullet && collision.CompareTag("Player"))
        {
            if (collision.GetComponentInParent<PlayerHealth>() is PlayerHealth player)
            {
                player.TakeDamage(damage);
                Debug.Log("💥 Hit PLAYER for " + damage + " damage.");
            }

            hasHit = true;
            Destroy(gameObject);
        }
        else
        {
            hasHit = true;
            Destroy(gameObject);
        }
    }
}
