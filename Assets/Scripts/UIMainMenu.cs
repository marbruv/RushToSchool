using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    public Button level2Button;
    public CanvasGroup level2CanvasGroup;
    public TMP_Text level2LockedText;
    public CanvasGroup level3CanvasGroup;
    public TMP_Text level3LockedText;

    void Start()
    {
        bool level1Completed = PlayerPrefs.GetInt("Level1Completed", 0) == 1;
        Debug.Log(level1Completed);

        level2Button.interactable = true;
        level2CanvasGroup.alpha = level1Completed ? 1f : 0.3f;

        level2LockedText.gameObject.SetActive(false);

        level3CanvasGroup.alpha = 0.3f;
    }
    
    public void PlayLevelRTS(int level)
    {
        HideAllLockedTexts();

        if (level == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        else if(level == 2)
        {
            // Check if Level 1 is completed
            if (PlayerPrefs.GetInt("Level1Completed", 0) == 1)
            {
                SceneManager.LoadScene("Level2");
            }
            else
            {
                level2LockedText.gameObject.SetActive(true);
                Invoke(nameof(HideLockedText), 2f);
            }
        }
        else
        {
            level3LockedText.gameObject.SetActive(true);
            Invoke(nameof(HideLevel3Text), 2f); // auto-hide after 2 seconds
            Debug.Log(level3LockedText);
        }
    }

    void HideLockedText()
    {
        level2LockedText.gameObject.SetActive(false);
    }

    void HideLevel3Text()
    {
        level3LockedText.gameObject.SetActive(false);
    }

    void HideAllLockedTexts()
    {
        CancelInvoke(); // cancels ALL pending Invoke calls
        
        level2LockedText.gameObject.SetActive(false);
        level3LockedText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
