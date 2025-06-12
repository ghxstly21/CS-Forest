using UnityEngine;

public class BigTommyMovement : MonoBehaviour
{
    public float speed = 8f;
    public float leftLimit = -10f;
    public float rightLimit = 10f;
    public int damage = 1;
    public float damageCooldown = 1f;

    public float groundY = 0f;  // Set this to your floor height

    private bool movingRight = true;
    private float lastDamageTime = -999f;

    private void Update()
    {
        // Move Big Tommy
        float moveDirection = movingRight ? 1f : -1f;
        transform.Translate(moveDirection * speed * Time.deltaTime, 0, 0);

        // Clamp vertical position to groundY
        Vector3 pos = transform.position;
        pos.y = groundY;
        transform.position = pos;

        // Flip direction at limits
        if (transform.position.x >= rightLimit)
        {
            movingRight = false;
            FlipSprite();
        }
        else if (transform.position.x <= leftLimit)
        {
            movingRight = true;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x = movingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                var playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
