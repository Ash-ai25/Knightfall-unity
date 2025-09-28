using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public Button retryButton;
    public Button quitButton;
    public TMP_Text gameOverText;

    [Header("Settings")]
    public string gameOverMessage = "Game Over";
    public float showDelay = 1.5f;

    private bool isInitialized = false;

    void Awake()
    {
        // Handle singleton pattern across scenes
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
    }

    void Initialize()
    {
        if (isInitialized) return;

        // Hide game over panel at start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("GameOverPanel is not assigned!");
        }

        // Setup button listeners
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(RetryGame);
        }
        else
        {
            Debug.LogError("RetryButton is not assigned!");
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("QuitButton is not assigned!");
        }

        isInitialized = true;
        Debug.Log("GameOverManager initialized successfully.");
    }

    void OnEnable()
    {
        // Re-initialize when enabled
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Make sure UI is hidden when a new scene loads
        if (gameOverPanel != null && gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(false);
        }

        // Ensure time scale is normal when loading new scenes
        Time.timeScale = 1f;

        Debug.Log("New scene loaded: " + scene.name);
    }

    public void ShowGameOver()
    {
        if (this == null) return;

        Debug.Log("Showing game over screen...");

        // Show game over screen after a delay
        Invoke("DisplayGameOver", showDelay);
    }

    void DisplayGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game over panel activated.");

            // Set text if available
            if (gameOverText != null)
            {
                gameOverText.text = gameOverMessage;
            }

            // Pause the game
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("Cannot display game over - panel reference is null!");
        }
    }

    public void RetryGame()
    {
        Debug.Log("Retrying game...");

        // Resume time before reloading
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");

        // Resume time before quitting
        Time.timeScale = 1f;

        // Quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Add this method to manually test the game over screen
    public void TestGameOver()
    {
        Debug.Log("Manually testing game over screen");
        ShowGameOver();
    }
}