using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitAndSetCharacter());
    }

    private IEnumerator WaitAndSetCharacter()
    {
        // Wait a couple frames to make sure the scene is fully initialized
        yield return null;
        yield return null;

        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("No player found in scene.");
            yield break;
        }

        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        if (sr != null && PlayerSelection.chosenSprite != null)
        {
            sr.sprite = PlayerSelection.chosenSprite;
            sr.flipX = PlayerSelection.chosenCharacterName == "Sam";
        }
        else
        {
            Debug.LogWarning("SpriteRenderer or chosen sprite is missing.");
        }
    }
}