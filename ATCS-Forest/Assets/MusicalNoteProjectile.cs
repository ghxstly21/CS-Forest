using UnityEngine;

public class MusicalNoteProjectile : MonoBehaviour
{
    public float damage = 0.5f;   // small decimal damage
    public float lifetime = 3f;   // destroy after 3 seconds

    void Start()
    {
        Destroy(gameObject, lifetime);  // auto destroy after some time
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Try to get player's health component
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);  // Destroy projectile after hitting player
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);  // Destroy if hits ground or walls
        }
    }
}
