using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 3;
    public float currentHealth;

    public GameObject gameOverPanel;
    public GameObject playerMovementScriptObject;

    void Start()
    {
        currentHealth = maxHealth;
        gameOverPanel.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage. Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 Player Died");

        if (playerMovementScriptObject != null)
        {
            playerMovementScriptObject.SetActive(false);
        }

        gameOverPanel.SetActive(true);
    }

    public void Respawn()
    {
        SceneManager.LoadScene(2);
    }

    // 👇 ADD THESE TWO METHODS
    public void RestoreToFull()
    {
        currentHealth = maxHealth;
        Debug.Log("❤️ Fully healed to " + currentHealth + " HP");
    }

    public void IncreaseMaxHealth()
    {
        maxHealth += 1;
        currentHealth = maxHealth;
        Debug.Log("💪 Max health increased to " + maxHealth + " and healed to full.");
    }
}
