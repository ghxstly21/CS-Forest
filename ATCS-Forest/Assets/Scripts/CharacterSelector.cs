using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public GameObject playerPrefab;
    public Sprite samSprite;
    public Sprite ryanSprite;
    public Sprite miguelSprite;
    public void SelectCharacter(string characterName)
    {
        if (characterName != "Sam" && characterName != "Ryan" && characterName != "Miguel") return;
        PlayerSelection.chosenCharacterName = characterName;
        switch (characterName)
        {
            case "Sam":
                PlayerSelection.chosenSprite = samSprite;
                break;
            case "Ryan":
                PlayerSelection.chosenSprite = ryanSprite;
                break;
            case "Miguel":
                PlayerSelection.chosenSprite = miguelSprite;
                break;
        }
        SceneManager.LoadScene(2); // Load level 1
    }
}