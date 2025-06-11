using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public Image healthBarFill; // Drag the red bar image here in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"🩸 Player took {damage} damage. Current health: {currentHealth}");

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Debug.Log("💀 Player died.");
            // Add death handling here if needed
        }
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("⚠️ Health bar fill image not assigned!");
        }
    }
}
