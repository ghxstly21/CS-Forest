using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManagerLevel1 : MonoBehaviour
{
    public GameObject playeryes;
    public GameObject storyPanel;
    public Button continueButton;
    public PlayerMovement2D player;
    public GameObject healthbackground;
    public TMP_Text storyText; 

    private string[] storyLines = {
    "Welcome to ATCS: Forest Trials!",
    "This is a 2D platformer-RPG hybrid where you play as an ATCS student of your choice.",
    "You must journey through a mysterious forest filled with challenges and secrets.",
    "But beware... not all ATCS students are on your side.",
    "Some have gone rogue — acting as bosses who block your path!",
    "Whenever you confront one, the world pauses and an RPG battle begins.",
    "You'll need strategy, timing, and courage to defeat them and move forward.",
    "Oh — and rumor has it, Mr. Sen is guarding something deep within the forest...",
    
    // First Level Introduction
    "Your journey begins at the edge of the forest.",
    "It seems quiet — too quiet...",
    "Suddenly, a swarm of smart drones appears!",
    "They're fast, adaptive, and programmed for one thing: stopping you.",
    "These drones are no accident — they were built by ATCS students.",
    "This is just the first test. Show them what you've got!"
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
