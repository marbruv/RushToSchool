using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string mainSceneName = "MainScene";

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
