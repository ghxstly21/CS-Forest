using UnityEngine;

public class HorizontalPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float patrolDistance = 5f;

    private Vector3 startPosition;
    private int direction = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, startPosition) >= patrolDistance)
        {
            direction *= -1;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}

