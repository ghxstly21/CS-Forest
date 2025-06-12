using UnityEngine;
using UnityEngine.InputSystem;
using hi;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f;
    public float gravityScale = 2f;
    public float minJumpHeight = 0.9f;
    public float maxJumpHeight = 3f;
    public GameObject player;


    private Rigidbody2D rb;
    private PlayerControls controls;
    private bool isGrounded;

    private Animator animator;  // Add this

    private bool isWalking = false;  // Track walking state

    void Awake()
    {
        player.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        rb.gravityScale = gravityScale;

        animator = GetComponent<Animator>();  // Get Animator component
        if (animator == null)
            Debug.LogWarning("Animator component not found on player.");
    }

    void OnEnable()
    {
        controls.Enable();
        controls.GamePlay.Jump.performed += OnJump;
        controls.GamePlay.Jump.canceled += OnJumpRelease;
    }

    void OnDisable()
    {
        controls.GamePlay.Jump.performed -= OnJump;
        controls.GamePlay.Jump.canceled -= OnJumpRelease;
        controls.Disable();
    }

    void Update()
    {
        Vector2 input = controls.GamePlay.Move.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(input.x * speed, rb.linearVelocity.y);

        if (rb.gravityScale != gravityScale)
            rb.gravityScale = gravityScale;

        // Flip sprite depending on direction
        if (input.x > 0.01f)
            transform.localScale = new Vector3(3, transform.localScale.y, transform.localScale.z);
        else if (input.x < -0.01f)
            transform.localScale = new Vector3(-3, transform.localScale.y, transform.localScale.z);

        // Update isWalking based on horizontal input
        isWalking = Mathf.Abs(input.x) > 0.01f;

        // Update animator parameter
        if (animator != null)
            animator.SetBool("isWalking", isWalking);
       
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (!isGrounded) return;

        float g = Mathf.Abs(Physics2D.gravity.y * gravityScale);
        float v = Mathf.Sqrt(2f * g * maxJumpHeight);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, v);
        isGrounded = false;
    }

    void OnJumpRelease(InputAction.CallbackContext ctx)
    {
        if (rb.linearVelocity.y > 0f)
        {
            float g = Mathf.Abs(Physics2D.gravity.y * gravityScale);
            float vMin = Mathf.Sqrt(2f * g * minJumpHeight);
            if (rb.linearVelocity.y > vMin)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, vMin);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
