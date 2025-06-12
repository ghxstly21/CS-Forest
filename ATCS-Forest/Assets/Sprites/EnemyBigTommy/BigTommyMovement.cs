using UnityEngine;

public class BigTommyRunBackAndForth : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkDistance = 1f;

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * speed, rb.linearVelocity.y);

        // Flip if no ground ahead or hit wall
        if (!IsGroundAhead() || HitWall())
        {
            Flip();
        }
    }

    bool IsGroundAhead()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, direction, checkDistance, groundLayer);
        return hit.collider != null;
    }

    bool HitWall()
    {
        Vector2 forward = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, forward, 0.5f, groundLayer);
        return wallHit.collider != null;
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth player = collision.collider.GetComponentInParent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
                Debug.Log("Big Tommy collided with the player!");
            }
        }
    }
}
