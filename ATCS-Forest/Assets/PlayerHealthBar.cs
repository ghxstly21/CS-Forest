using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 1.5f, 0);
    public Image healthFillImage;

    private PlayerHealth playerHealth;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned!");
            enabled = false;
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not found on player!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        Vector3 worldPos = player.position + offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;

        // Update fill amount
        float fillAmount = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        healthFillImage.fillAmount = fillAmount;
    }
}
