using UnityEngine;

public class ProjectileNew : MonoBehaviour
{
    public bool isEnemyBullet = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemyBullet && other.CompareTag("Enemy"))
        {
            EnemyHelicopter enemy = other.GetComponent<EnemyHelicopter>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
                Destroy(gameObject);  // Destroy projectile on hit so it doesn't hit multiple times
            }
        }
        else if (isEnemyBullet && other.CompareTag("Player"))
        {
            // Handle damage to player here if you want
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
