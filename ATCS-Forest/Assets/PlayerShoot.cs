using UnityEngine;
using UnityEngine.InputSystem;
using hi;
public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;   // The bullet prefab (must have Rigidbody2D)
    public Transform shootPoint;          // Where the bullet comes from
    public float shootCooldown = 0.5f;    // Time between shots
    public float bulletSpeed = 10f;       // Speed of the bullet

    private float cooldownTimer = 0f;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void Start()
    {
        controls.GamePlay.Shoot.performed += ctx => Shoot();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    void Shoot()
    {
        if (cooldownTimer > 0f) return;

        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogError("Projectile prefab or shoot point is not assigned!");
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;

        Vector3 direction = (mousePos - shootPoint.position).normalized;

        GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        cooldownTimer = shootCooldown;
    }
}
