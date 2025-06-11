using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public BossHelicopter boss; // Drag in the Inspector
    public Image fillImage;     // Drag the HealthBarFill image here

    private void Update()
    {
        if (boss != null && fillImage != null)
        {
            fillImage.fillAmount = (float)boss.CurrentHealth / boss.MaxHealth;
        }
    }
}
