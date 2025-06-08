using hi;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f;
    public float gravityScale = 1f;
    public float minJumpHeight = 6f;
    public float maxJumpHeight = 8f;

    Rigidbody2D rb;
    PlayerControls controls;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        rb.gravityScale = gravityScale;
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
        // horizontal movement
        Vector2 input = controls.GamePlay.Move.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(input.x * speed, rb.linearVelocity.y);

        // keep gravityScale updated if you change it at runtime
        if (rb.gravityScale != gravityScale) 
            rb.gravityScale = gravityScale;
    }

    void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (!isGrounded) return;

        float g = Mathf.Abs(Physics2D.gravity.y * gravityScale);
        // compute initial velocity to reach maxJumpHeight
        float v = Mathf.Sqrt(2f * g * maxJumpHeight);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, v);
        isGrounded = false;
    }

    void OnJumpRelease(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        // if still moving upward, clamp to the minimum jump height
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