using UnityEngine;
using UnityEngine.InputSystem;
using hi;

public class PlayerMovementAndCamera : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Transform cameraTransform;

    private Vector2 movement;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        controls.GamePlay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => movement = Vector2.zero;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        float move = movement.x;

        transform.position += new Vector3(move * moveSpeed * Time.deltaTime, 0f, 0f);

        if (move > 0) spriteRenderer.flipX = false;
        else if (move < 0) spriteRenderer.flipX = true;

        animator.SetBool("isWalking", move != 0);

        cameraTransform.position = new Vector3(transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
    }
}
