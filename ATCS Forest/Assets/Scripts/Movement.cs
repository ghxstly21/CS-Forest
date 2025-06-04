using hi;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rb;
    PlayerControls controls;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        Vector2 input = controls.GamePlay.Move.ReadValue<Vector2>();
        rb.velocity = new Vector2(input.x * speed, rb.velocity.y);
    }
}
