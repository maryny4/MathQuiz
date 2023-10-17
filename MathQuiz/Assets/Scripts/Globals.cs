using System;
using System.Collections.Generic;
using UnityEngine;
public class Globals : MonoBehaviour
{
    public static Globals instance;

    public RiddleDataList riddleDataList;
    public float transitionDuration = .38f;
    public float transitionDelay = .12f; 

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Destroying duplicate Globals object - only one is allowed per scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }

    public int GetBestScore(int score)
    {
        int bestScore = PlayerPrefs.GetInt("SCORE");
        
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("SCORE", bestScore);
        }

        return bestScore;
    }
}

public static class GameAction
{
    public static Action<int> onClickAnswer;
    public static Action<List<string>> setAnswers;
    public static Action showTransitionScreen;
    public static Action<int, int> setButtonsColor;//1 - correct button / 2 - wrong button
    public static Action timeIsOver;
    public static Action<bool> startGame;
}