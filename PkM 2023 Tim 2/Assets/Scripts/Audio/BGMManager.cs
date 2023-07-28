using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    // List of scene names where BGM should stop
    public string[] scenesWithoutBGM;
    private static BGMManager instance;
    private AudioSource BGM;

    private void Awake()
    {
        // Check if an instance of the BGM GameObject already exists
        if (instance == null)
        {
            instance = this;
            BGM = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate BGM GameObject
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene's name is in the list of scenesToStopBGM
        foreach (string sceneName in scenesWithoutBGM)
        {
            if (scene.name == sceneName)
            {
                BGM.Stop();
                return;
            }
        }

        // If the current scene is not in the list, continue playing BGM
        if (!BGM.isPlaying)
        {
            BGM.Play();
        }
    }
}
