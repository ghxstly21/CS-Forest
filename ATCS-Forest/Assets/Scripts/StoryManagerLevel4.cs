using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManagerLevel4 : MonoBehaviour
{
    public GameObject playeryes;
    public GameObject storyPanel;
    public Button continueButton;
    public PlayerMovement2D player;
    public GameObject healthbackground;
    public TMP_Text storyText; // Use this for your story display

    private string[] storyLines = {
    "You beat the entire forest.",
    "You not only shot down the smart drones but also killed the inventors (f students)",
    "But you forgot that there is always someone supervising students",
    "Baba Sen is very mad...",
    "I hoped you like the enemies because they're coming back from the grave..",
    "As Baba Sen says, 'CommandZ'",
    "The only way to end this all.. is to kill him",
    "Brace yourself. This is the END.",
    "Good luck."
};


    private int currentLineIndex = 0;

    void Awake()
    {
        healthbackground.SetActive(false);
    }

    void Start()
    {
        storyPanel.SetActive(true);
        Time.timeScale = 0f;
        player.enabled = false;
        playeryes.SetActive(false);
        storyText.text = storyLines[currentLineIndex]; // Show the first line
        continueButton.onClick.AddListener(AdvanceStory);
    }

    void AdvanceStory()
    {
        currentLineIndex++;

        if (currentLineIndex < storyLines.Length)
        {
            storyText.text = storyLines[currentLineIndex]; // Show next line
        }
        else
        {
            CloseStory(); // End of story
        }
    }

    void CloseStory()
    {
        storyPanel.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        playeryes.SetActive(true);
        healthbackground.SetActive(true);
    }
}
