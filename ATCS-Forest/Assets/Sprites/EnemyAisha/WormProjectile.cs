using UnityEngine;

public class WormProjectile : MonoBehaviour
{
    public float crawlSpeed = 1f;
    public float damagePerSecond = 0.2f;
    public float maxLifetime = 8f;

    private Transform player;
    private bool attached = false;
    private float lifetime;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        lifetime = maxLifetime;
    }

    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        if (!attached)
        {
            // Move toward player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * crawlSpeed * Time.deltaTime;

            // Attach if close enough
            if (Vector2.Distance(transform.position, player.position) < 0.4f)
            {
                attached = true;
                offset = transform.position - player.position; // Lock current position offset
            }
        }
        else
        {
            // Stay attached to the player
            transform.position = player.position + offset;

            // Deal damage over time
            PlayerHealth ph = player.GetComponentInParent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }

        // Count down lifetime
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
