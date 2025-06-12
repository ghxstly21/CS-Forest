using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManagerLevel3 : MonoBehaviour
{
    public GameObject playeryes;
    public GameObject storyPanel;
    public Button continueButton;
    public PlayerMovement2D player;
    public GameObject healthbackground;
    public TMP_Text storyText; // Use this for your story display

    private string[] storyLines = {
    "You beat the two freaks, Miguel and Aisha.",
    "However, there's a bigger enemy in the forest",
    "I hope you can jump...",
    "because Big Tommy is very very mad.",
    "You defeated his food and he needs a new food",
    "which is you..",
    "Press space to jump his rolls",
    "Brace yourself. This is only the beginning.",
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
