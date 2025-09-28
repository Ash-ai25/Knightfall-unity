using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader2D : MonoBehaviour
{
    public static SceneLoader2D Instance;

    [Header("Transition Settings")]
    public float fadeDuration = 1.0f;
    public CanvasGroup fadeCanvasGroup;

    private string targetScene;
    private bool isTransitioning = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Fade in from black when scene starts
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
            StartCoroutine(FadeIn());
        }
    }

    public void LoadScene(string sceneName)
    {
        if (isTransitioning) return;

        targetScene = sceneName;
        isTransitioning = true;

        // Start fade out process
        if (fadeCanvasGroup != null)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            // If no fade canvas, load directly
            SceneManager.LoadScene(sceneName);
            isTransitioning = false;
        }
    }

    private IEnumerator FadeIn()
    {
        fadeCanvasGroup.blocksRaycasts = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator FadeOut()
    {
        fadeCanvasGroup.blocksRaycasts = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
        SceneManager.LoadScene(targetScene);

        // After scene loads, fade back in
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (fadeCanvasGroup != null)
        {
            StartCoroutine(FadeIn());
        }

        isTransitioning = false;
    }
}