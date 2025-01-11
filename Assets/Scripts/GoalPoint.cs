using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SceneManagement.GoalColors currentGoalColor;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = SceneManagement.Instance.ConvertToColorValues(currentGoalColor);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManagement.Instance.FinishedCurrentColor(currentGoalColor);
        }
        SceneManagement.Instance.LoadScene();
    }
}
