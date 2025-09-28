using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : Entity_Health
{
    void Start()
    {
        // Test if GameOverManager is accessible
        if (GameOverManager.Instance != null)
        {
            Debug.Log("GameOverManager found successfully!");
        }
        else
        {
            Debug.LogError("GameOverManager NOT found. Please add a GameManager object with GameOverManager script to the scene.");
        }
    }

    protected override void Die()
    {
        base.Die();

        // Trigger game over screen
        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.ShowGameOver();
        }
        else
        {
            Debug.LogError("GameOverManager instance not found at time of death!");

            // Fallback: reload scene after delay
            Invoke("ReloadSceneFallback", 2f);
        }

    }

    void ReloadSceneFallback()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}