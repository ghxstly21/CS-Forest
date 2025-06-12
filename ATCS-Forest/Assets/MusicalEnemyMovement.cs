using UnityEngine;

public class MusicalEnemyMovement : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float patrolDistance = 4f;
    public float bobAmplitude = 0.5f;
    public float bobFrequency = 2f;

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Horizontal patrol back and forth
        transform.position += Vector3.right * patrolSpeed * direction * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolDistance)
        {
            direction *= -1;  // Reverse direction
            FlipSprite();
        }

        // Vertical bobbing motion
        float newY = startPos.y + Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;  // Flip horizontally
        transform.localScale = scale;
    }
}
