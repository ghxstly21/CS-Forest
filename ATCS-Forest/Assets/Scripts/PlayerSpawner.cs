using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // assigned in inspector
    //Setting spawn position; can be changed later
    private static float xPosition = -7.5f; 
    private static float yPosition = -3.3f;
    private Vector3 spawnPosition = new Vector3(xPosition, yPosition, 1);

    void Start()
    {
        if (playerPrefab is null)
        {
            Debug.LogError("Player prefab not assigned!");
            return;
        }

        GameObject player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        if (sr is not null && PlayerSelection.chosenSprite is not null)
        {
            sr.sprite = PlayerSelection.chosenSprite;
            if (PlayerSelection.chosenCharacterName == "Sam")
            {
                sr.flipX = true;  // flips The Sam to face right
            }
            else
            {
                sr.flipX = false; // default facing direction
            }
        }
        else
        {
            Debug.LogWarning("SpriteRenderer or chosenSprite missing!");
        }
       
    }
}