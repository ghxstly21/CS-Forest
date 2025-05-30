using UnityEngine;

public class ScrollingBackground2D : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float backgroundWidth = 20f; 
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= -backgroundWidth)
        {
            transform.position += new Vector3(2 * backgroundWidth, 0, 0);
        }
    }
}

