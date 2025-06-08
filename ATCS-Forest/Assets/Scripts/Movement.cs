using UnityEngine;
using hi;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private PlayerControls controls;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        controls.GamePlay.Enable(); 
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        Vector2 input = controls.GamePlay.Move.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(input.x * speed, rb.linearVelocity.y);

        if (input.x > 0.01f)
            transform.localScale = new Vector3(3, transform.localScale.y, transform.localScale.z);
        else if (input.x < -0.01f)
            transform.localScale = new Vector3(-3, transform.localScale.y, transform.localScale.z);
    }
}
