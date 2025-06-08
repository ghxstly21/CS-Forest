using UnityEngine;
using UnityEngine.InputSystem;
using hi;
public class PlayerJumpNoGravity : MonoBehaviour
{
    public float jumpHeight = 2f;      // Jump height
    public float jumpDuration = 0.5f;  // Time to jump up and down

    private bool isJumping = false;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
        controls.GamePlay.Jump.performed += ctx => OnJump();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void OnJump()
    {
        if (!isJumping)
        {
            StartCoroutine(JumpRoutine());
        }
    }

    private System.Collections.IEnumerator JumpRoutine()
    {
        isJumping = true;

        Vector3 startPos = transform.position;
        Vector3 peakPos = startPos + Vector3.up * jumpHeight;

        float halfDuration = jumpDuration / 2f;
        float timer = 0f;

        while (timer < halfDuration)
        {
            transform.position = Vector3.Lerp(startPos, peakPos, timer / halfDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = peakPos;

        timer = 0f;
        while (timer < halfDuration)
        {
            transform.position = Vector3.Lerp(peakPos, startPos, timer / halfDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;

        isJumping = false;
    }
}
