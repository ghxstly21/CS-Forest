using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public TMP_Text enemiesLeftText;
    public int nextSceneIndex; 

    private bool sceneLoading = false;

    void Update()
    {
        int enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesLeftText.text = "Enemies Left: " + enemiesLeft;

        if (enemiesLeft == 0 && !sceneLoading)
        {
            sceneLoading = true;
            StartCoroutine(LoadNextSceneAsync());
        }
    }

    private System.Collections.IEnumerator LoadNextSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
