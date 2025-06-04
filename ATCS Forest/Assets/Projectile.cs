using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 50f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
        }
    }
}
