using UnityEngine;

public class MusicalNoteProjectile : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    // Assign this from EnemyShoot script based on note type
    public Sprite noteSprite;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        if (noteSprite != null)
            spriteRenderer.sprite = noteSprite;

        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming player has a PlayerHealth script
            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground") || other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
