using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SceneManagement.GoalColors currentGoalColor;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_spriteRenderer.color = SceneManagement.Instance.ConvertToColorValues(currentGoalColor);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // SceneManagement.Instance.FinishedCurrentColor(currentGoalColor);
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Tutorial") {
              SceneManager.LoadScene("Mechanics");
            }
            if (currentScene.name == "Mechanics") {
                SceneManager.LoadScene("Panels 2");
            }

        }
        // SceneManagement.Instance.LoadScene();
    }
}
