using UnityEngine;

public class QuitMenuButton : MonoBehaviour
{
    public void QuitGame()
    {
        // Quit the application
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
