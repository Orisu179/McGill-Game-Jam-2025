using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SceneManagement.GoalColors currentGoalColor;
    [SerializeField] private GameObject effect;
    private bool doneLevel;
    private EffectsAudioControl _effectsAudioControl;
    void Start()
    {
        //_spriteRenderer.color = SceneManagement.Instance.ConvertToColorValues(currentGoalColor);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           _effectsAudioControl = collision.gameObject.GetComponentInChildren<EffectsAudioControl>();
        }
        if(!doneLevel){
            StartCoroutine(EndLevel());
            doneLevel = true;
        }
    }

    private IEnumerator EndLevel(){
        CameraScript.Shake(2);
        Instantiate(effect, transform.position, Quaternion.identity);
        if (_effectsAudioControl != null)
        {
            _effectsAudioControl.PlayCollisionSound("Finish");
            _effectsAudioControl.PlayCollisionSound("NewLevel");
        }

        yield return null;

        // SceneManagement.Instance.FinishedCurrentColor(currentGoalColor);
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Tutorial") {
            SceneManager.LoadScene("Picture");
        }
        else if (currentScene.name == "Mechanics") {
            SceneManager.LoadScene("Panels 2");
        }
        else if (currentScene.name == "Panels 2") {
            SceneManager.LoadScene("ThoughtLevel");
        }
        else if(currentScene.name == "ThoughtLevel"){
            SceneManager.LoadScene("super Hue");
        }
        else if(currentScene.name == "super Hue"){
            SceneManager.LoadScene("Portal");
        }
        else if(currentScene.name == "super Hue"){
            SceneManager.LoadScene("Ending");
        }

        // SceneManagement.Instance.LoadScene();
    }
}
