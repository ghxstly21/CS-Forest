using UnityEngine;
using UnityEngine.InputSystem;
using hi;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;

    [Header("Stats")]
    public float shootCooldown = 0.5f;
    public static float bulletSpeed = 10f;
    public static int projectileCount = 1;
    public static int damage = 1;

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

        float spreadAngle = 10f;
        float startAngle = -(spreadAngle * (projectileCount - 1)) / 2f;

        for (int i = 0; i < projectileCount; i++)
        {
            float angleOffset = startAngle + i * spreadAngle;
            Quaternion rotation = Quaternion.AngleAxis(angleOffset, Vector3.forward);
            Vector3 shotDirection = rotation * direction;

            GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = shotDirection * bulletSpeed;

            float angle = Mathf.Atan2(shotDirection.y, shotDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            ProjectileNew proj = bullet.GetComponent<ProjectileNew>();
            if (proj != null)
                proj.damage = damage;
        }

        cooldownTimer = shootCooldown;
    }

    public void IncreaseProjectileCount() => projectileCount++;
    public void IncreaseDamage() => damage++;
    public void IncreaseBulletSpeed() => bulletSpeed += 5f;
}
