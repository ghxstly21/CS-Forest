using UnityEngine;

public class AishaEnemy : MonoBehaviour
{
    public GameObject wormProjectilePrefab;
    public Transform shootPoint;
    public float shootCooldown = 3f;
    public int maxHealth = 3;
    public int xpDrop = 30;

    private float timer;
    private int currentHealth;
    private bool isInvincible = false;
    private float invincibilityDuration = 0.5f;
    private float invincibilityTimer = 0f;

    private void Start()
    {
        timer = shootCooldown;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
                isInvincible = false;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ShootWorm();
            timer = shootCooldown;
        }

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            float direction = player.position.x < transform.position.x ? -1f : 1f;
            transform.localScale = new Vector3(1.5f * direction, 1.5f, 1.5f);
        }

    }

    void ShootWorm()
    {
        if (wormProjectilePrefab != null && shootPoint != null)
        {
            Instantiate(wormProjectilePrefab, shootPoint.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isInvincible) return;

        currentHealth -= dmg;
        Debug.Log($"🪱 Aisha took {dmg} damage. HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        // Trigger invincibility frames
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

        // Optional: add flash or flicker animation here
    }

    void Die()
    {
        Debug.Log("🪦 Aisha died!");

        // Give XP
        PlayerXP xp = FindAnyObjectByType<PlayerXP>();
        if (xp != null)
            xp.GainXP(xpDrop);

        Destroy(gameObject);
    }
}
