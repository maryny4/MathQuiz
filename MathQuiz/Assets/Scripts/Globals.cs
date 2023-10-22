using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Globals : MonoBehaviour
{
    public static Globals instance;

    public RiddleDataList riddleDataList;
    public float transitionDuration = .38f;
    public float transitionDelay = .12f;

    private int RewardCoins;
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

    ///negative range
    private bool negativeRange;

    public void NegativeRangeSwitcher()
    {
        negativeRange = !negativeRange;
        PlayerPrefs.SetInt("NEGATIVE_RANGE", negativeRange ? 1 : 0);
    }
    public bool NegativeRangeState => negativeRange;

    
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
        negativeRange = PlayerPrefs.GetInt("NEGATIVE_RANGE") == 1;
    }
    
    /// <param name="coinsToAdd"></param>
    public void AddCoins(int coinsToAdd)
    {
        int currentCoins = PlayerPrefs.GetInt("COINS", 0); // Получаем текущее количество монет
        currentCoins += coinsToAdd; // Увеличиваем монеты на coinsToAdd
        PlayerPrefs.SetInt("COINS", currentCoins); // Сохраняем обновленное значение в PlayerPrefs
        UpdateCoinsDisplay(); // Вызываем функцию для обновления отображения монет в интерфейсе (если есть)
        RewardCoins = coinsToAdd;
    }
    
    [ContextMenu("X_2adb")]
    public void X2_Coin()
    {
        AddCoins(RewardCoins);
    }

    public void UpdateCoinsDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt("COINS", 0);
    }
    ///

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
    public static Action<HINT_TYPE> useHint;
    public static Action<int, int> disableHalfAnswers;
    public static Action<int> showCorrectAnswer;
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

public enum HINT_TYPE
{
    FULL_HINT,
    NEW_ANSWER,
    FIFTY_FIFTY
}