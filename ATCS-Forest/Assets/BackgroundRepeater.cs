using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    public int repeatCount = 10;
    public GameObject backgroundPrefab;

    void Start()
    {
        Vector3 startPos = transform.position;
        float width = backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        for (int i = -repeatCount; i <= repeatCount; i++)
        {
            Vector3 pos = startPos + Vector3.right * width * i;
            Instantiate(backgroundPrefab, pos, Quaternion.identity, transform);
        }
    }
}
