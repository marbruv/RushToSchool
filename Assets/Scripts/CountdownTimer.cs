using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountdownTimerTMP : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 10f;
    private float timeRemaining;

    [Header("UI References")]
    public TMP_Text timeText;           // TMP text for countdown
    public GameObject gameOverPanel;    // Game over panel
    public TMP_Text resultText;         // TMP text to show "LEVEL COMPLETED" or "LEVEL FAILED"

    void Start()
    {
        timeRemaining = startTime;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeText.text = Mathf.Ceil(timeRemaining).ToString();
        }
        else
        {
            timeRemaining = 0;
            timeText.text = "0";
            ShowLevelFailed();  
        }
    }

    // Call this when the player fails (timer reaches 0)
    void ShowLevelFailed()
    {
        gameOverPanel.SetActive(true);
        resultText.text = "LEVEL FAILED!";
        Time.timeScale = 0f;
    }

    // Call this from another script when the player completes the level
    public void ShowLevelCompleted()
    {
        gameOverPanel.SetActive(true);
        resultText.text = "LEVEL COMPLETED!";
        Time.timeScale = 0f;
    }
}
