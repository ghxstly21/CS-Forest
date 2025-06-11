using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene(1);
    }
}
