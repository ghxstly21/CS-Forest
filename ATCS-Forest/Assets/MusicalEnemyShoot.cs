using UnityEngine;

public class MusicalEnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab;    // The musical note prefab
    public Transform shootPoint;
    public float shootCooldown = 0.3f;     // Rapid fire
    private float shootTimer;

    public Sprite[] noteSprites;            // Assign different note sprites here

    private int currentNoteIndex = 0;

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        GameObject note = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        MusicalNoteProjectile proj = note.GetComponent<MusicalNoteProjectile>();
        if (proj != null)
        {
            // Cycle through note sprites
            proj.noteSprite = noteSprites[currentNoteIndex];
            currentNoteIndex = (currentNoteIndex + 1) % noteSprites.Length;
        }
    }
}
