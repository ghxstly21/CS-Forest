using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerXP : MonoBehaviour
{
    private PlayerShoot shoot;
    private PlayerHealth health;

    [Header("XP and Level")]
    public static int currentLevel = 1;
    public float currentXP = 0f;
    public static float xpToNextLevel = 100f;

    [Header("UI Elements")]
    public Slider xpBar;
    public TMP_Text levelText;

    [Header("Level Up Panel")]
    public GameObject levelUpPanel;
    public Button[] upgradeButtons; // assign in Inspector
    public TMP_Text[] upgradeNames; // assign in Inspector
    public TMP_Text[] upgradeDescriptions; // assign in Inspector

    private List<UpgradeOption> allUpgrades;

    void Start()
    {
        shoot = FindFirstObjectByType<PlayerShoot>();
        health = FindFirstObjectByType<PlayerHealth>();

        levelUpPanel.SetActive(false);
        UpdateUI();

        allUpgrades = new List<UpgradeOption>()
        {
            new UpgradeOption(
                "Projectile Prodigy",
                () => $"Fire {PlayerShoot.projectileCount + 1} projectiles per shot.",  // dynamic description
                () => { shoot.IncreaseProjectileCount(); }
            ),
            new UpgradeOption(
                "Sharpshooter",
                () => "Your shots fly faster.",
                () => { shoot.IncreaseBulletSpeed(); }
            ),
            new UpgradeOption(
                "Heavy Damager",
                () => "Shots deal more damage.",
                () => { shoot.IncreaseDamage(); }
            ),
            new UpgradeOption(
                "Recharge",
                () => "Instantly recover all health.",
                () => { health.RestoreToFull(); }
            ),
            new UpgradeOption(
                "Battery Boost",
                () => "Increase your maximum and current health by 5 HP.",
                () => { health.IncreaseMaxHealth(); }
            ),
        };
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
            xpToNextLevel += (1.5f*xpToNextLevel);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        xpBar.value = (float)currentXP / xpToNextLevel;
        levelText.text = $"Level {currentLevel}";
    }

    void LevelUp()
    {
        currentLevel++;
        Time.timeScale = 0f; // Pause game
        levelUpPanel.SetActive(true);
        ShowUpgradeChoices();
    }

    void ShowUpgradeChoices()
    {
        // Pick 3 unique random upgrades
        List<int> chosenIndexes = new List<int>();
        while (chosenIndexes.Count < 3)
        {
            int randomIndex = Random.Range(0, allUpgrades.Count);
            if (!chosenIndexes.Contains(randomIndex))
                chosenIndexes.Add(randomIndex);
        }

        for (int i = 0; i < 3; i++)
        {
            int index = chosenIndexes[i];
            UpgradeOption option = allUpgrades[index];

            upgradeNames[i].text = option.Title;
            upgradeDescriptions[i].text = option.GetDescription();

            upgradeButtons[i].onClick.RemoveAllListeners();
            int buttonIndex = i; // avoid closure issue
            upgradeButtons[i].onClick.AddListener(() =>
            {
                ChooseUpgrade(allUpgrades[chosenIndexes[buttonIndex]]);
            });
        }
    }

    public void ChooseUpgrade(UpgradeOption upgrade)
    {
        upgrade.ApplyUpgrade.Invoke();
        CloseLevelUpPanel();
    }

    void CloseLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f; // Resume game
        UpdateUI();
    }
}

public class UpgradeOption
{
    public string Title;
    public System.Func<string> GetDescription;
    public System.Action ApplyUpgrade;

    public UpgradeOption(string title, System.Func<string> getDescription, System.Action applyUpgrade)
    {
        Title = title;
        GetDescription = getDescription;
        ApplyUpgrade = applyUpgrade;
    }
}
