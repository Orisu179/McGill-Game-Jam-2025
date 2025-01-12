using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnEscape : MonoBehaviour
{
    public string sceneToLoad = "Pause"; // The name of the pause menu scene
    private Scene currentScene; // Store the current scene

    private static LoadSceneOnEscape instance; // Singleton instance

    void Awake()
    {
        // Ensure only one instance of this script exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        // Get the current active scene at the start
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Load the pause menu scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Call this function to resume the current scene
    public void Resume()
    {
        // If the scene is already loaded, don't try to load it again
        if (SceneManager.GetActiveScene().name != currentScene.name)
        {
            // Load the current scene again to "resume"
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
