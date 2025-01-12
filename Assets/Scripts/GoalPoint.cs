using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SceneManagement.GoalColors currentGoalColor;
    private bool doneLevel;
    public GameObject effect;
    void Start()
    {
        //_spriteRenderer.color = SceneManagement.Instance.ConvertToColorValues(currentGoalColor);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!doneLevel){
            StartCoroutine(endLevel());
            doneLevel = true;
        }
    }

    IEnumerator endLevel(){
        CameraScript.Shake(2);
        Instantiate(effect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);

        // SceneManagement.Instance.FinishedCurrentColor(currentGoalColor);
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Tutorial") {
            SceneManager.LoadScene("Mechanics");
        }
        else if (currentScene.name == "Mechanics") {
            SceneManager.LoadScene("Panels 2");
        }
        else if (currentScene.name == "Panels 2") {
            SceneManager.LoadScene("ThoughtLevel");
        }
        // SceneManagement.Instance.LoadScene();
    }
}
