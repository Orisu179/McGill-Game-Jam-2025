using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum GoalColors
    {
        Violet = 1,
        Indigo = 2,
        Blue = 3,
        Green = 4,
        Yellow = 5,
        Orange = 6,
        Red = 7
    }

    private struct ColorData
    {
        public Color Color;
        public bool Eaten;
    }

    public static SceneManagement Instance;
    private Dictionary<GoalColors, ColorData> _achievedColors;
    private int _sceneCounter;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        _sceneCounter = 2;
        _achievedColors = new Dictionary<GoalColors, ColorData>();
        InitColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           LoadScene();
        }
    }

    private void InitColor()
    {
        _achievedColors.Clear();
        _achievedColors.Add(GoalColors.Violet, new ColorData { Color = new Color(127, 0, 255), Eaten = false });
        _achievedColors.Add(GoalColors.Indigo, new ColorData { Color = new Color(75, 0, 130), Eaten = false });
        _achievedColors.Add(GoalColors.Blue, new ColorData { Color = Color.blue, Eaten = false });
        _achievedColors.Add(GoalColors.Green, new ColorData { Color = Color.green, Eaten = false });
        _achievedColors.Add(GoalColors.Yellow, new ColorData { Color = Color.yellow, Eaten = false });
        _achievedColors.Add(GoalColors.Orange, new ColorData { Color = new Color(255, 165, 0), Eaten = false });
        _achievedColors.Add(GoalColors.Red, new ColorData { Color = Color.red, Eaten = false });
    }

    public bool HaveAchievedColor(GoalColors color)
    {
        return _achievedColors[color].Eaten;
    }

    public void FinishedCurrentColor(GoalColors color)
    {
        if (_achievedColors[color].Eaten)
            return;
        _achievedColors[color] = new ColorData { Color = _achievedColors[color].Color, Eaten = true };
        _sceneCounter++;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneCounter);
    }

    public Color ConvertToColorValues(GoalColors color)
    {
        return !_achievedColors.TryGetValue(color, out var achievedColor) ? Color.black : achievedColor.Color;
    }
}