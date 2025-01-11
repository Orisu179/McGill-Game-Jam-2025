using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button _button;
    void Start()
    {
       _button = GetComponent<Button>();
       _button.onClick.AddListener(OnButtonClicked);
       _button.GetComponentInChildren<TextMeshProUGUI>().text = "Reset";
    }

    private static void OnButtonClicked()
    {
        SceneManagement.Instance.LoadScene();
    }
}
