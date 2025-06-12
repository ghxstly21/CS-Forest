using UnityEngine;

public class BigTommyRollingMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 3f;
    public float torqueAmount = 20f;
    public float stopDistance = 0.1f;

    private Rigidbody2D rb;
    private Vector2 currentTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (pointA != null && pointB != null)
            currentTarget = pointB.position;
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        Vector2 direction = ((Vector2)currentTarget - rb.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // Apply torque to simulate rolling
        float torqueDir = -Mathf.Sign(direction.x); // Flip direction depending on target
        rb.AddTorque(torqueDir * torqueAmount);

        // Flip direction if close to target
        if (Vector2.Distance(rb.position, currentTarget) < stopDistance)
        {
            currentTarget = (currentTarget == (Vector2)pointA.position) ? pointB.position : pointA.position;
        }

        // Flip visual scale for sprite if needed (optional)
        transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth player = collision.collider.GetComponentInParent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1); // You can change this value as needed
                Debug.Log("Big Tommy hit the player!");
            }
        }
    }
}
