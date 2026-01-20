using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    [Header("UI Texts")]
    public TMP_Text resultText;  // To show "LEVEL FAILED!" or "LEVEL COMPLETED!"

    [Header("Game Over Image")]
    [SerializeField] private Image gameOverImage; 
    [SerializeField] private Sprite defaultGameOverSprite;

    [Header("Winning Image")]
    [SerializeField] private Sprite WinningSprite;
    [SerializeField] private AudioClip WinningSfx;

    [Header("Outside road")]
    [SerializeField] private Sprite OutsideRoad;
    [SerializeField] private AudioClip OutsideRoadSfx;

    [Header("Audio")]
    [SerializeField] private AudioSource uiAudioSource;   
    [SerializeField] private AudioClip defaultGameOverSfx;

    [Header("Scenes")]
    public string mainMenuScene = "MainMenu";

    [Header("Music")]
    public SceneMusic sceneMusic;

    [Header("Lane Check")]
    public BikeLaneChecker laneChecker;

    [Header("HUD / Gameplay UI")]
    [SerializeField] private GameObject gameplayUI;

    private bool isPaused = false;
    private bool isGameOver = false;

    void Update()
    {
        // Prevent UI selection lock
        if (EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        // ESC toggles pause ONLY if game is NOT over
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (laneChecker != null && !laneChecker.onGrass)
        {
            PlayerHitObstacle(OutsideRoad, OutsideRoadSfx);
        }

        if (!isGameOver && !isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            SFXPlayer.Instance.PlayRingBell();
        }
    }

    // -----------------------------
    //          PAUSE SYSTEM
    // -----------------------------

    private void SetGameplayUIVisible(bool visible)
    {
        if (gameplayUI != null)
            gameplayUI.SetActive(visible); // [web:39]
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        if (isGameOver) return;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        SetGameplayUIVisible(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (isGameOver) return;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        SetGameplayUIVisible(true);

        Time.timeScale = 1f;
        isPaused = false;
    }


    // -----------------------------
    //          GAME OVER
    // -----------------------------
    public void TriggerGameOver(string message, Sprite gameOverSprite = null, AudioClip gameOverSfx = null)
    {
        if (isGameOver) return;
        isGameOver = true;

        // sound
        if (uiAudioSource != null)
        {
            var clipToPlay = (gameOverSfx != null) ? gameOverSfx : defaultGameOverSfx;
            if (clipToPlay != null)
                uiAudioSource.PlayOneShot(clipToPlay); // [web:47]
        }

        isPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false);

        SetGameplayUIVisible(false);

        if (resultText != null)
            resultText.text = message;
        
        SFXPlayer.Instance.PlayCrash();
        if (resultText != null)
            resultText.text = message;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        if (sceneMusic != null)
            sceneMusic.PlayGameOverMusic();

        // Stop game
        if (resultText != null) resultText.text = message;

        if (gameOverImage != null)
            gameOverImage.sprite = (gameOverSprite != null) ? gameOverSprite : defaultGameOverSprite;

        // Stop game
        Time.timeScale = 0f;
    }


    // -----------------------------
    //       GAME FLOW
    // -----------------------------
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // -----------------------------
    //       PLAYER COLLISIONS
    // -----------------------------
    // Call this from your Player script (or from a collider handler attached to the player)
    public void PlayerHitObstacle()
    {
        TriggerGameOver("LEVEL FAILED!");
    }

    public void PlayerHitObstacle(Sprite obstacleSprite, AudioClip obstacleSfx)
    {
        TriggerGameOver("LEVEL FAILED!", obstacleSprite, obstacleSfx);
    }

    public void PlayerReachesGoal()
    {
        PlayerPrefs.SetInt("Level1Completed", 1);
        PlayerPrefs.Save();
        
        TriggerGameOver("LEVEL COMPLETED!", WinningSprite, WinningSfx);
    }
}
