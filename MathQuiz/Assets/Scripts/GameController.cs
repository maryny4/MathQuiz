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

    private List<string> shuffledCurrentAnswers;
    [SerializeField] private List<string> Answers;
    private Riddle currentRiddle = new Riddle();

    void Start()
    {
        GameAction.onClickAnswer += CheckAnswer;
        GameAction.startGame += StartGame;
        StartGame();
    }

    private void OnDestroy()
    {
        GameAction.onClickAnswer -= CheckAnswer;
        GameAction.startGame -= StartGame;
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
                riddle = EndlessMode_example;
                answers = EndlessMode_list;
                break;
            case GAME_MODE.X_3:
                (string X_3_example, double X_3_result) = FunctionForGenerator.X_3(MinValueWithandWithoutBrackets,
                    MaxValueWithandWithoutBrackets, MinRangeResult, MaxRangeResult);
                (List<string> X_3_list, Dictionary<double, List<string>> resultMapping_X_3) =
                    ListForGenerator.GenerateListsAndMappings(X_3_result, MinValue, MaxValue);
                riddle = X_3_example;
                answers = X_3_list;
                break;
            case GAME_MODE.FIND_X:
                (string FIND_X_example, double FIND_X_result) =
                    FunctionForGenerator.GenerateEquation_Find_X(MinValue, MaxValue);
                (List<string> FIND_X_List, Dictionary<double, List<string>> resultMapping_FIND_X_example) =
                    ListForGenerator.GenerateListsAndMappings(FIND_X_result, MinValue, MaxValue);
                riddle = FIND_X_example;
                answers = FIND_X_List;
                break;
            case GAME_MODE.SUM_AND_SUBTRACT:
                (string sumAndSubtractExample, double sumAndSubtractResult) =
                    FunctionForGenerator.GenerateSumAndSubtractRiddle(MinValue, MaxValue);
                (List<string> sumAndSubtractList,
                        Dictionary<double, List<string>> resultMapping_sumAndSubtractExample) =
                    ListForGenerator.GenerateListsAndMappings(sumAndSubtractResult, MinValue, MaxValue);
                riddle = sumAndSubtractExample;
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
                riddle = multiplicationAndDivisionExample;
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
}