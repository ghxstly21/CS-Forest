using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public GameObject gameOverPanel;  // Assign this in the inspector
    public GameObject playerMovementScriptObject;  // Optional: drag the player object to disable movement

    void Start()
    {
        currentHealth = maxHealth;
        gameOverPanel.SetActive(false); // Hide Game Over UI at start
    }

    public void TakeDamage(int amount)
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

        // Disable player movement script (optional)
        if (playerMovementScriptObject != null)
        {
            playerMovementScriptObject.SetActive(false);
        }

        // Show Game Over panel
        gameOverPanel.SetActive(true);
    }

    // Called by UI button
    public void Respawn()
    {
        SceneManager.LoadScene(2); // Scene 2 = Level 1
    }
}
