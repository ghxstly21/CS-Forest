using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManagerLevel2 : MonoBehaviour
{
    public GameObject playeryes;
    public GameObject storyPanel;
    public Button continueButton;
    public PlayerMovement2D player;
    public GameObject healthbackground;
    public TMP_Text storyText;

    private string[] storyLines = {
    "Congratulations on defeating the smart drones!",
    "Those drones were tough to beat, but you pulled through.",
    "However... there's more you should know.",
    "Those drones were created by the ATCS kids—deep in the forest.",
    "And now, they're mad. Really mad.",
    "These kids usually hate the wilderness...",
    "But their rage has driven them out of their labs to hunt you down.",
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
