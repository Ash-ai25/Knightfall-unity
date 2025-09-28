using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
        public float volume = 1f;
        public bool loop = true;
    }

    [Header("Music Settings")]
    public SceneMusic[] sceneMusicList;
    public AudioSource audioSource;
    public float fadeDuration = 1f;

    private string currentSceneName;

    void Awake()
    {
        // Singleton pattern - only one music manager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Create audio source if not assigned
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.loop = true;
                audioSource.playOnAwake = false;
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Play music for the initial scene
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        // Don't change music if we're already playing the right track
        if (currentSceneName == sceneName && audioSource.isPlaying)
            return;

        currentSceneName = sceneName;

        // Find music for this scene
        SceneMusic musicForScene = null;
        foreach (SceneMusic sceneMusic in sceneMusicList)
        {
            if (sceneMusic.sceneName == sceneName)
            {
                musicForScene = sceneMusic;
                break;
            }
        }

        // If no specific music found for this scene, look for a default
        if (musicForScene == null)
        {
            foreach (SceneMusic sceneMusic in sceneMusicList)
            {
                if (sceneMusic.sceneName == "Default")
                {
                    musicForScene = sceneMusic;
                    break;
                }
            }
        }

        // Play the music
        if (musicForScene != null && musicForScene.musicClip != null)
        {
            StartCoroutine(CrossFadeMusic(musicForScene.musicClip, musicForScene.volume, musicForScene.loop));
        }
        else
        {
            // Stop music if no clip found for this scene
            StartCoroutine(FadeOutMusic());
        }
    }

    IEnumerator CrossFadeMusic(AudioClip newClip, float volume, bool loop)
    {
        // Fade out current music
        if (audioSource.isPlaying)
        {
            float startVolume = audioSource.volume;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            audioSource.Stop();
        }

        // Play new music
        audioSource.clip = newClip;
        audioSource.volume = 0f; // Start at 0 volume for fade in
        audioSource.loop = loop;
        audioSource.Play();

        // Fade in new music
        float elapsedTimeIn = 0f;
        while (elapsedTimeIn < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, volume, elapsedTimeIn / fadeDuration);
            elapsedTimeIn += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = volume;
    }

    IEnumerator FadeOutMusic()
    {
        if (audioSource.isPlaying)
        {
            float startVolume = audioSource.volume;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            audioSource.Stop();
        }
    }

    // Optional: Methods to control music from other scripts
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }

    public void StopMusic()
    {
        StartCoroutine(FadeOutMusic());
    }
}