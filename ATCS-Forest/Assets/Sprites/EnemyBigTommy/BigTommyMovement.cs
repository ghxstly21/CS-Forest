using UnityEngine;

public class BigTommyPatrol : MonoBehaviour
{
    public Transform pointA; // Left patrol point
    public Transform pointB; // Right patrol point
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Transform targetPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPoint = pointB; // Start heading right
    }

    void FixedUpdate()
    {
        if (targetPoint == null) return;

        // Move toward the target point
        Vector2 direction = (targetPoint.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // Flip sprite based on direction
        if (direction.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        // Switch target when close
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }
    }
}
