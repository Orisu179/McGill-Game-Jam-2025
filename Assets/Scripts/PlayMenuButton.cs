using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);
    }
}
