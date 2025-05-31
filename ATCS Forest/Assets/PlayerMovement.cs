using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float moveDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveDirection = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1f;
            animator.Play("PlayerWalk");
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1f;
            animator.Play("PlayerWalk");
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.Play("PlayerIdle");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
    }
}
