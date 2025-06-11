using UnityEngine;

public class OrbitAroundBoss : MonoBehaviour
{
    public Transform center;
    public float radius = 2f;
    public float speed = 1f;
    private float angle;

    void Update()
    {
        if (center == null) return;

        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = center.position + new Vector3(x, y, 0f);
    }
}
