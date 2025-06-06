using hi;
using UnityEngine;
using UnityEngine.InputSystem; // IMPORTANT

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootCooldown = 0.5f;

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

    GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

    if (transform.localScale.x < 0)
    {
        Vector3 bulletScale = bullet.transform.localScale;
        bulletScale.x *= -1;
        bullet.transform.localScale = bulletScale;

        Projectile projScript = bullet.GetComponent<Projectile>();
        if (projScript != null)
            projScript.speed *= -1;
    }

    cooldownTimer = shootCooldown;
}

}
