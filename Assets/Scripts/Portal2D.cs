using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal2D : MonoBehaviour
{
    public string targetSceneName;

    void Start()
    {
        Debug.Log("2D Portal initialized. Waiting for player...");

        // Check if we have a collider
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("No 2D collider attached to the portal! Please add a Collider2D component.");
        }
        else if (!GetComponent<Collider2D>().isTrigger)
        {
            Debug.LogError("Portal collider is not set as a trigger! Please check the 'Is Trigger' box.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered the portal: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal! Loading scene: " + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.Log("Not the player. Tag was: " + other.tag);
        }
    }

    // Draw a visible outline in the Scene view
    void OnDrawGizmos()
    {
        // Only draw gizmo if we have a collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, collider.bounds.size);
        }
    }
}