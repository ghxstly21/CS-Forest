using UnityEngine;

public class EndCheck : MonoBehaviour
{
    public float checkInterval = 1f;
    private bool gameEnded = false;

    private void Start()
    {
        InvokeRepeating(nameof(CheckEnemies), 0f, checkInterval);
    }

    void CheckEnemies()
    {
        if (gameEnded) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.Log("✅ All enemies defeated!");
            FindObjectOfType<EndGameSequence>()?.TriggerWin();
            gameEnded = true;
        }
    }
}
