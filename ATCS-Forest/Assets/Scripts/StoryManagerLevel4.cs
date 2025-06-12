using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections;

public class StoryManagerLevel4 : MonoBehaviour
{
    public GameObject playeryes;
    public GameObject storyPanel;
    public Button continueButton;
    public PlayerMovement2D player;
    public GameObject healthbackground;
    public TMP_Text storyText;
    public GameObject music;

    // 🎯 Add these for your images
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    private string[] storyLines = {
        "You beat the entire forest.",
        "You not only shot down the smart drones but also defeated the inventors (f students)",
        "But you forgot that there is always someone supervising students",
        "Baba Sen is very mad...",
        "I hoped you like the enemies because they're coming back from the grave..",
        "As Baba Sen says, 'CommandZ'",
        "The only way to end this all.. is to defeat him",
        "Brace yourself. This is the END.",
        "Good luck."
    };

    private int currentLineIndex = 0;

    void Awake()
    {
        healthbackground.SetActive(false);
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
    }

    void Start()
    {
        music.SetActive(false);
        storyPanel.SetActive(true);
        Time.timeScale = 0f;
        player.enabled = false;
        playeryes.SetActive(false);
        storyText.text = storyLines[currentLineIndex];
        continueButton.onClick.AddListener(AdvanceStory);
    }

    void AdvanceStory()
    {
        currentLineIndex++;

        if (currentLineIndex < storyLines.Length)
        {
            storyText.text = storyLines[currentLineIndex];
        }
        else
        {
            StartCoroutine(FlashImagesAndCloseStory());
        }
    }

    IEnumerator FlashImagesAndCloseStory()
    {
                storyPanel.SetActive(false);

        // Flash each image one by one
        image1.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        image1.SetActive(false);

        image2.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        image2.SetActive(false);

        image3.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        image3.SetActive(false);

        CloseStory();
    }

    void CloseStory()
    {
        music.SetActive(true);
        storyPanel.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        playeryes.SetActive(true);
        healthbackground.SetActive(true);
    }
}
