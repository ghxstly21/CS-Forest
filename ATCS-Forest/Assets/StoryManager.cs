using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public GameObject playeryes;
    public GameObject storyPanel;
    public Button continueButton;
    public PlayerMovement2D player;
    public GameObject healthbackground;
    void Awake()
    {
        healthbackground.SetActive(false);
    }

    void Start()
    {
        storyPanel.SetActive(true);
        Time.timeScale = 0f;
        player.enabled = false;

        continueButton.onClick.AddListener(CloseStory);
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
