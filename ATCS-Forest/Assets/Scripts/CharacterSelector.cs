using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public Sprite samSprite;
    public Sprite ryanSprite;
    public Sprite miguelSprite;
    public void SelectCharacter(string characterName)
    {
        if (characterName != "Sam" && characterName != "Ryan" && characterName != "Miguel")
        {
            Debug.Log("Unknown character selected");
            return;
        }
        PlayerSelection.chosenCharacterName = characterName;
        switch (characterName)
        {
            case "Sam":
                PlayerSelection.chosenSprite = samSprite;
                Debug.Log("Sam selected");
                break;
            case "Ryan":
                PlayerSelection.chosenSprite = ryanSprite;
                Debug.Log("Ryan selected");
                break;
            case "Miguel":
                PlayerSelection.chosenSprite = miguelSprite;
                Debug.Log("Miguel selected");
                break;
            default:
                Debug.Log("Unknown character selected");
                break;
        }
        SceneManager.LoadScene(2); // Load level 1
    }
}