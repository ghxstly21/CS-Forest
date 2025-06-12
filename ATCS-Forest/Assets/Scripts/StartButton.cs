using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void Go()
    {
        SceneManager.LoadScene(2);
    }
}
