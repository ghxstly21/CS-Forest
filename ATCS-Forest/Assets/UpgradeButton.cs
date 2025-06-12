using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    private UpgradeOption upgrade;
    private PlayerXP playerXP;

    // Set the upgrade data and update UI texts
    public void SetUpgrade(UpgradeOption upgrade, PlayerXP playerXP)
    {
        this.upgrade = upgrade;
        this.playerXP = playerXP;
        titleText.text = upgrade.Title;
        descriptionText.text = upgrade.GetDescription();
    }

    // Called when this button is clicked
    public void OnClick()
    {
        if (playerXP != null && upgrade != null)
        {
            playerXP.ChooseUpgrade(upgrade);
        }
    }
}
