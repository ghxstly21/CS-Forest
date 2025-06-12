using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public BossHelicopter boss; 
    public Image fillImage;
    private void Update()
    {
        if (boss != null && fillImage != null)
        {
            fillImage.fillAmount = (float)boss.CurrentHealth / boss.MaxHealth;
        }
    }
}
