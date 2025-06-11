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

    if (!isEnemyBullet)
    {
        // Check for Boss first
        BossHelicopter boss = collision.GetComponent<BossHelicopter>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Debug.Log("💥 Hit BOSS for " + damage + " damage.");
            hasHit = true;
            Destroy(gameObject);
            return;
        }

        // Then check for regular enemies
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
        }
        hasHit = true;
        Destroy(gameObject);
    }

    // Hit something else (e.g. wall)
    if (!hasHit)
    {
        Debug.Log("🧱 Projectile hit " + collision.name + " and was destroyed.");
        hasHit = true;
        Destroy(gameObject);
    }
}


}
