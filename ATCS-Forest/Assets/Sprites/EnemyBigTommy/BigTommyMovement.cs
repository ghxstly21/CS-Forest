using UnityEngine;

public class BigTommyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    public float leftLimit = -10f;
    public float rightLimit = 10f;
    public float groundY = 0f;  
    private bool movingRight = true;

    [Header("Attack")]
    public int damage = 1;
    public float damageCooldown = 1f;
    private float lastDamageTime = -999f;

    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Move();
        StickToGround();
    }

    private void Move()
    {
        float moveDirection = movingRight ? 1f : -1f;
        transform.Translate(moveDirection * speed * Time.deltaTime, 0, 0);

        if (transform.position.x >= rightLimit)
        {
            movingRight = false;
            FlipSprite();
        }
        else if (transform.position.x <= leftLimit)
        {
            movingRight = true;
            FlipSprite();
        }
    }

    private void StickToGround()
    {
        Vector3 pos = transform.position;
        pos.y = groundY;
        transform.position = pos;
    }

    private void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x = movingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time - lastDamageTime >= damageCooldown)
        {
            var playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastDamageTime = Time.time;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(" Big Tommy took " + amount + " damage! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(" Big Tommy is DEAD!");
        Destroy(gameObject);
    }
}
