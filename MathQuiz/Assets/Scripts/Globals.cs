using System;
using System.Collections.Generic;
using UnityEngine;
public class Globals : MonoBehaviour
{
    public static Globals instance;

    public RiddleDataList riddleDataList;
    public float transitionDuration = .38f;
    public float transitionDelay = .12f;
    /// GAME_MODE
    private GAME_MODE currentGameMode;
    public GAME_MODE GetCurrentGameMode => currentGameMode;
    public void SetCurrentGameMode(GAME_MODE gameMode) => currentGameMode = gameMode;
    /// rangeOfDifficulty
    private int rangeOfDifficulty;
    public void SetRangeOfDifficulty(int value)
    {
        rangeOfDifficulty = value;
        PlayerPrefs.SetInt("rangeOfDifficulty", rangeOfDifficulty);
    }
    public int GetRangeOfDifficulty => rangeOfDifficulty;
    ///
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
        rangeOfDifficulty = PlayerPrefs.GetInt("rangeOfDifficulty", 15);
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
    public static Action<bool> soundEnable;
}

public enum GAME_MODE
{
    FULL,
    FIND_X,
    DOUBLE,
    X_3,
    SUM_AND_SUBTRACT,
    MULTIPLICATION_AND_DIVISION,
}