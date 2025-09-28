using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleScreenManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject titleScreenPanel;
    public Button startButton;
    public Button quitButton;
    public TMP_Text titleText;
    public string gameSceneName = "SampleScene"; // Change to your first level name

    [Header("Settings")]
    public float fadeDuration = 1f;
    public CanvasGroup fadeCanvasGroup;

    void Start()
    {
        // Setup button listeners
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }

        // Make sure title screen is visible
        if (titleScreenPanel != null)
        {
            titleScreenPanel.SetActive(true);
        }

        // Initialize fade canvas
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.gameObject.SetActive(false);
        }

        // Ensure time is running normally
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        Debug.Log("Starting game...");

        if (fadeCanvasGroup != null)
        {
            StartCoroutine(FadeAndLoadScene());
        }
        else
        {
            // Load directly if no fade canvas
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private System.Collections.IEnumerator FadeAndLoadScene()
    {
        fadeCanvasGroup.gameObject.SetActive(true);

        // Fade to black
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;

        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game from title screen...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Optional: Add keyboard support
    void Update()
    {
        // Press Space or Enter to start game
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        // Press Escape to quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}