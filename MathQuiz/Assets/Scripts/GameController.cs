using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private TextMeshProUGUI riddleText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image timerBar;
    [SerializeField] private CanvasGroup answersCanvasGroup;

    [SerializeField] private float timeToAnswer = 7f;
    private float timer;
    private bool useTimer = true;
    private bool useSecondLife = false;
    private int score;
    private bool isPaused = false;
    private DateTime pauseTime;
    private DateTime resumeTime;
    private int tempScore;


    private List<string> shuffledCurrentAnswers;
    [SerializeField] private List<string> Answers;
    private Riddle currentRiddle = new Riddle();

    void Start()
    {
        //PlayerPrefs.SetInt("COINS", 500);
        //PlayerPrefs.Save();
        GameAction.onClickAnswer += CheckAnswer;
        GameAction.startGame += StartGame;
        GameAction.useHint += UseHint;

        StartGame();
    }

    private void OnDestroy()
    {
        GameAction.onClickAnswer -= CheckAnswer;
        GameAction.startGame -= StartGame;
        GameAction.useHint -= UseHint;
    }

    private void Update()
    {
        Timer();
    }

    private void StartGame(bool clearScore = true)
    {
        answersCanvasGroup.interactable = true;
        if (clearScore) score = 0;
        StartCoroutine(TextUpdater.UpdateText(scoreText, score.ToString()));
        useTimer = true;
        useSecondLife = false;
        ResetTimer();
        GenerateRiddle();
    }

    void CheckAnswer(int answerIndex)
    {
        if (shuffledCurrentAnswers[answerIndex] == currentRiddle.GetAnswers[0]) AnsweredCorrectly();
        else AnsweredWrongly(answerIndex);
    }


    void GenerateRiddle()
    {
        int MinValue = Globals.instance.NegativeRangeState ? -Globals.instance.GetRangeOfDifficulty : 0;
        int MaxValue = Globals.instance.GetRangeOfDifficulty;
        int MinValueWithandWithoutBrackets = MinValue;
        int MaxValueWithandWithoutBrackets = MaxValue;
        int MinRangeResult = MinValue;
        int MaxRangeResult = MaxValue;
        const decimal FloatingPointValue1 = 0.1M;
        const decimal FloatingPointValue2 = 0.25M;
        const decimal FloatingPointValue3 = 0.5M;
        const decimal FloatingPointValue4 = 0.75M;

        string riddle = "riddle";
        List<string> answers = new List<string> { "correct", "wrong", "wrong1", "wrong2" };

        switch (Globals.instance.GetCurrentGameMode)
        {
            case GAME_MODE.FULL:
                (string EndlessMode_example, double EndlessMode_result) = FunctionForGenerator.EndlessMode(MinValue,
                    MaxValue, MinValueWithandWithoutBrackets, MaxValueWithandWithoutBrackets, MinRangeResult,
                    MaxRangeResult, FloatingPointValue1, FloatingPointValue2, FloatingPointValue3, FloatingPointValue4);
                (List<string> EndlessMode_list, Dictionary<double, List<string>> resultMapping_EndlessMode) =
                    ListForGenerator.GenerateListsAndMappings(EndlessMode_result, MinValue, MaxValue);
                riddle = Checkingbrackets.CheckingbracketsProcess(EndlessMode_example);
                answers = EndlessMode_list;
                break;
            case GAME_MODE.X_3:
                (string X_3_example, double X_3_result) = FunctionForGenerator.X_3(MinValueWithandWithoutBrackets,
                    MaxValueWithandWithoutBrackets, MinRangeResult, MaxRangeResult);
                (List<string> X_3_list, Dictionary<double, List<string>> resultMapping_X_3) =
                    ListForGenerator.GenerateListsAndMappings(X_3_result, MinValue, MaxValue);
                riddle = Checkingbrackets.CheckingbracketsProcess(X_3_example);
                answers = X_3_list;
                break;
            case GAME_MODE.FIND_X:
                (string FIND_X_example, double FIND_X_result) =
                    FunctionForGenerator.GenerateEquation_Find_X(MinValue, MaxValue);
                (List<string> FIND_X_List, Dictionary<double, List<string>> resultMapping_FIND_X_example) =
                    ListForGenerator.GenerateListsAndMappings(FIND_X_result, MinValue, MaxValue);
                riddle = Checkingbrackets.CheckingbracketsProcess(FIND_X_example);
                answers = FIND_X_List;
                break;
            case GAME_MODE.SUM_AND_SUBTRACT:
                (string sumAndSubtractExample, double sumAndSubtractResult) =
                    FunctionForGenerator.GenerateSumAndSubtractRiddle(MinValue, MaxValue);
                (List<string> sumAndSubtractList,
                        Dictionary<double, List<string>> resultMapping_sumAndSubtractExample) =
                    ListForGenerator.GenerateListsAndMappings(sumAndSubtractResult, MinValue, MaxValue);

                riddle = Checkingbrackets.CheckingbracketsProcess(sumAndSubtractExample);
                answers = sumAndSubtractList;
                break;
            case GAME_MODE.DOUBLE:
                (string doubleExample, double doubleResult) =
                    FunctionForGenerator.GenerateDoubleRiddle(MinValue, MaxValue);
                (List<string> doubleList, Dictionary<double, List<string>> resultMappingDouble) =
                    ListForGenerator.GenerateListsAndMappings(doubleResult, MinValue, MaxValue);
                riddle = doubleExample;
                answers = doubleList;
                break;
            case GAME_MODE.MULTIPLICATION_AND_DIVISION:
                (string multiplicationAndDivisionExample, double multiplicationAndDivisionResult) =
                    FunctionForGenerator.GenerateMultiplicationAndDivisionRiddle(MinValue, MaxValue);
                (List<string> multiplicationAndDivisionList,
                        Dictionary<double, List<string>> resultMappingMultiplicationAndDivision) =
                    ListForGenerator.GenerateListsAndMappings(multiplicationAndDivisionResult, MinValue, MaxValue);
                riddle = Checkingbrackets.CheckingbracketsProcess(multiplicationAndDivisionExample);
                answers = multiplicationAndDivisionList;
                break;
        }


        riddle = riddle.Replace('/', '÷');
        currentRiddle.SetRiddle(riddle);
        currentRiddle.SetAnswers(answers);

        shuffledCurrentAnswers = new List<string>(currentRiddle.GetAnswers);
        ListShuffler.Shuffle(shuffledCurrentAnswers);
        GameAction.setAnswers?.Invoke(shuffledCurrentAnswers);
        StartCoroutine(TextUpdater.UpdateText(riddleText, currentRiddle.GetRiddle));
    }

    void AnsweredCorrectly()
    {
        Debug.Log("AnsweredCorrectly");
        score++;

        // Добавляем монету только в случае верного ответа и быстрее 5 секунд
        float timeSpent = timeToAnswer - timer;
        int coinsToAdd = 0;
        if (timeSpent < 5.0f)
        {
            coinsToAdd = 2;
        }
        else
        {
            coinsToAdd = 1;
        }

        Globals.instance.AddCoins(coinsToAdd);

        StartCoroutine(TextUpdater.UpdateText(scoreText, score.ToString()));
        ResetTimer();
        GenerateRiddle();
    }

    void AnsweredWrongly(int wrongAnswer)
    {
        Debug.Log("AnsweredWrongly");
        answersCanvasGroup.interactable = false;
        GameAction.setButtonsColor(FindCorrecAnswer(), wrongAnswer);
        useTimer = false;
        gameOverPanel.ShowPanelWithDelay(score, "WRONG ANSWER");
        Debug.Log("time has passed");
    }


    int FindCorrecAnswer()
    {
        for (int i = 0; i < shuffledCurrentAnswers.Count; i++)
            if (shuffledCurrentAnswers[i] == currentRiddle.GetAnswers[0])
                return i;

        return 10;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Приложение свернуто
            isPaused = true;
            pauseTime = DateTime.Now;
        }
        else
        {
            // Приложение возобновлено
            isPaused = false;
            resumeTime = DateTime.Now;

            // Рассчитайте разницу между временем при сворачивании и возобновлении
            TimeSpan timeDifference = resumeTime - pauseTime;

            // Вычитайте разницу из вашего таймера (или обновите его)
            timer -= (float)timeDifference.TotalSeconds;
        }
    }

    void Timer()
    {
        if (!useTimer) return;

        timer -= Time.deltaTime;

        timer = Math.Clamp(timer, 0, timeToAnswer);
        timerBar.fillAmount = timer / timeToAnswer;

        if (timer <= 0)
        {
            useTimer = false;
            answersCanvasGroup.interactable = false;
            GameAction.timeIsOver?.Invoke();
            gameOverPanel.ShowPanelWithDelay(score, "TIME IS OVER");
            Debug.Log("time has passed");
        }
    }

    void ResetTimer()
    {
        useTimer = true;
        timer = timeToAnswer;
    }

    void UseHint(HINT_TYPE hintType)
    {
        int totalCoins = PlayerPrefs.GetInt("COINS", 0);
        int hintCost = 0;

        switch (hintType)
        {
            case HINT_TYPE.FULL_HINT:
                hintCost = 25;
                break;
            case HINT_TYPE.NEW_ANSWER:
                hintCost = 5;
                break;
            case HINT_TYPE.FIFTY_FIFTY:
                hintCost = 15;
                break;
        }

        if (hintCost > 0)
        {
            if (totalCoins >= hintCost)
            {
                // Выполнить подсказку в соответствии с ее типом
                switch (hintType)
                {
                    case HINT_TYPE.FULL_HINT:
                        GameAction.showCorrectAnswer(FindCorrecAnswer());
                        break;
                    case HINT_TYPE.NEW_ANSWER:
                        StartGame(false);
                        break;
                    case HINT_TYPE.FIFTY_FIFTY:

                        int correctAnswer = FindCorrecAnswer();
                        int randomWrongAnswer1 = Random.Range(0, shuffledCurrentAnswers.Capacity);
                        int randomWrongAnswer2 = Random.Range(0, shuffledCurrentAnswers.Capacity);

                        while (correctAnswer == randomWrongAnswer1)
                        {
                            randomWrongAnswer1 = Random.Range(0, shuffledCurrentAnswers.Capacity);
                        }

                        while (correctAnswer == randomWrongAnswer2 || randomWrongAnswer1 == randomWrongAnswer2)
                        {
                            randomWrongAnswer2 = Random.Range(0, shuffledCurrentAnswers.Capacity);
                        }

                        GameAction.disableHalfAnswers?.Invoke(randomWrongAnswer1, randomWrongAnswer2);
                        break;
                }

                // Вычесть стоимость подсказки из монет
                Globals.instance.AddCoins(-hintCost);
                Debug.Log($"Hint used. Cost: {hintCost} coins. Remaining coins: {totalCoins}");
                Debug.Log("Total Coins: " + totalCoins);
            }
            else
            {
                Debug.Log("Not enough coins for this hint.");
                Debug.Log("Total Coins: " + totalCoins);
            }
        }
    }
}