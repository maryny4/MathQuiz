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
    
    private List<string> shuffledCurrentAnswers;
    [SerializeField]private List<string> Answers;
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
        if(clearScore) score = 0;
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
    ///work
(string example, double result) EndlessMode(int min, int max, int minWithBrackets, int maxWithBrackets, int minRandomExample, int maxRandomExample, decimal value1, decimal value2, decimal value3, decimal value4)
{
    int randomChoice = Random.Range(1, 7);

    string example = "";
    double result = 0;

    switch (randomChoice)
    {
        case 1:
            example = AdditionSubtractionExpressionGenerator.GenerateAdditionSubtractionExpression(min, max);
            result = AdditionSubtractionExpressionGenerator.CalculateAdditionSubtractionExpression(example);
            break;

        case 2:
            example = MultiplicationDivisionExpressionGenerator.GenerateMultiplicationDivisionExpression(min, max);
            result = MultiplicationDivisionExpressionGenerator.CalculateMultiplicationDivisionExpression(example);
            break;

        case 3:
            example = ExpressionWithoutBracketsGenerator.GenerateExpressionWithoutBrackets(minWithBrackets, maxWithBrackets, minRandomExample, maxRandomExample);
            result = ExpressionWithoutBracketsGenerator.CalculateExpressionWithoutBrackets(example);
            break;

        case 4:
            example = ExpressionWithBracketsGenerator.GenerateExpressionWithBrackets(minWithBrackets, maxWithBrackets, minRandomExample, maxRandomExample);
            result = ExpressionWithBracketsGenerator.CalculateExpressionBrackets(example);
            break;

        case 5:
            int randomFunctionChoice = Random.Range(1, 6);

            switch (randomFunctionChoice)
            {
                case 1:
                    example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(value1, minRandomExample, maxRandomExample);
                    result = (double)value1;
                    break;

                case 2:
                    example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(value2, minRandomExample, maxRandomExample);
                    result = (double)value2;
                    break;

                case 3:
                    example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(value3, minRandomExample, maxRandomExample);
                    result = (double)value3;
                    break;

                case 4:
                    example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(value4, minRandomExample, maxRandomExample);
                    result = (double)value4;
                    break;
            }
            break;
        
        case 6:
            example = GeneratorEquation_Find_X.GenerateRandomEquation_Find_X(min, max);
            result = GeneratorEquation_Find_X.CalculateEquationEquation_Find_X(example);
            break;
    }

    return (example, result);
}


/// Work
(string example, double result) X_3(int MinValueWithandWithoutBrackets, int MaxValueWithandWithoutBrackets, int MinRangeResult, int MaxRangeResult)
{
    int randomChoice = Random.Range(3, 5);  // Теперь случаи 3 и 4

    ///если minRange равно -30 и maxRange равно 30, то генератор будет генерировать арифметические выражения, такие как "5 + 6 * 7", и проверять,
    /// что результат этого выражения находится в диапазоне от -30 до 30.
    /// Если результат выражения не соответствует этому диапазону, генерация будет повторена до тех пор, пока не будет получено подходящее выражение.
    /// MinValueRandomExample MinRange
    /// MaxValueRandomExample MaxRange
    string example = "";
    double result = 0;
    switch (randomChoice)
    {
        case 3:
            example = ExpressionWithoutBracketsGenerator.GenerateExpressionWithoutBrackets(MinValueWithandWithoutBrackets, MaxValueWithandWithoutBrackets, MinRangeResult, MaxRangeResult);
            result = ExpressionWithoutBracketsGenerator.CalculateExpressionWithoutBrackets(example);
            break;

        case 4:
            example = ExpressionWithBracketsGenerator.GenerateExpressionWithBrackets(MinValueWithandWithoutBrackets, MaxValueWithandWithoutBrackets, MinRangeResult, MaxRangeResult);
            result = ExpressionWithBracketsGenerator.CalculateExpressionBrackets(example);
            break;
    }

    return (example, result);
}

(string example, double result) GenerateEquation_Find_X(int minValue, int maxValue)
{
    string example = "";
    double result = 0;
    example = GeneratorEquation_Find_X.GenerateRandomEquation_Find_X(minValue, maxValue);
    result = GeneratorEquation_Find_X.CalculateEquationEquation_Find_X(example);
    return (example, result);
}
// Work
    (string example, double result) GenerateSumAndSubtractRiddle(int minValue, int maxValue)
    {
        string example = "";
        double result = 0;
        example = AdditionSubtractionExpressionGenerator.GenerateAdditionSubtractionExpression(minValue, maxValue);
        result = AdditionSubtractionExpressionGenerator.CalculateAdditionSubtractionExpression(example);
        return (example, result);
    }
// Work
(string example, double result) GenerateDoubleRiddle(int minDouble , int maxDouble)
{
    int randomChoice = Random.Range(1, 5);  // Выбираем случайную функцию

    string example = "";
    double result = 0;

    switch (randomChoice)
    {
        case 1:
            example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(0.1M, minDouble, maxDouble);
            result = (double)0.1M;
            break;

        case 2:
            example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(0.25M, minDouble, maxDouble);
            result = (double)0.25M;
            break;

        case 3:
            example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(0.5M, minDouble, maxDouble);
            result = (double)0.5M;
            break;

        case 4:
            example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(0.75M, minDouble, maxDouble);
            result = (double)0.75M;
            break;
    }

    return (example, result);
}



// Функция для генерации задачи в режиме MULTIPLICATION_AND_DIVISION
(string example, double result) GenerateMultiplicationAndDivisionRiddle(int minValue, int maxValue)
{
    string example = "";
    double result = 0;
    example = MultiplicationDivisionExpressionGenerator.GenerateMultiplicationDivisionExpression(minValue, maxValue);
    result = MultiplicationDivisionExpressionGenerator.CalculateMultiplicationDivisionExpression(example);
    return (example, result);
}
    
    
    void GenerateRiddle()
    {
        const int MinValue = 1;
        const int MaxValue = 30;
        const int MinValueWithandWithoutBrackets = 1;
        const int MaxValueWithandWithoutBrackets = 15;
        const int MinRangeResult = 1;   ///Диапазон ответов
        const int MaxRangeResult = 15; ///Диапазон ответов
        const decimal FloatingPointValue1 = 0.1M;
        const decimal FloatingPointValue2 = 0.25M;
        const decimal FloatingPointValue3 = 0.5M;
        const decimal FloatingPointValue4 = 0.75M;
        string riddle = "riddle";
        List<string> answers = new List<string>{ "correct", "wrong", "wrong1", "wrong2" };
        double result1 = 0.1;
        double result2 = 0.25;
        double result3 = 0.5;
        double result4 = 0.75;
        switch (Globals.instance.GetCurrentGameMode)
        {
            case GAME_MODE.FULL:
                (string EndlessMode_example, double EndlessMode_result) = EndlessMode(MinValue,MaxValue,MinValueWithandWithoutBrackets,MaxValueWithandWithoutBrackets,MinRangeResult,MaxRangeResult,FloatingPointValue1,FloatingPointValue2,FloatingPointValue3,FloatingPointValue4);
                List<string> EndlessMode_list = new List<string>();
                Dictionary<double, List<string>> resultMapping_EndlessMode = new Dictionary<double, List<string>>
                {
                    { result1, new List<string> { result1.ToString(), result2.ToString(), result3.ToString(), result4.ToString() } },
                    { result2, new List<string> { result2.ToString(), result1.ToString(), result3.ToString(), result4.ToString() } },
                    { result3, new List<string> { result3.ToString(), result1.ToString(), result2.ToString(), result4.ToString() } },
                    { result4, new List<string> { result4.ToString(), result1.ToString(), result2.ToString(), result3.ToString() } }
                };

                if (resultMapping_EndlessMode.ContainsKey(EndlessMode_result))
                {
                    EndlessMode_list.AddRange(resultMapping_EndlessMode[EndlessMode_result]);
                }

                else
                {
                    // Добавляем значения, округленные до целых чисел
                    if (EndlessMode_result == 0)
                    {
                        EndlessMode_list.Add(EndlessMode_result.ToString());
                        EndlessMode_list.Add(Math.Round(EndlessMode_result + 2).ToString()); // Замена умножения на сложение 2
                        EndlessMode_list.Add(Math.Round(EndlessMode_result - 2).ToString()); // Замена деления на вычитание 2
                        EndlessMode_list.Add(Math.Round(EndlessMode_result - 5).ToString());
                    }
                    else
                    {
                        EndlessMode_list.Add(EndlessMode_result.ToString());
                        EndlessMode_list.Add(Math.Round(EndlessMode_result * 2).ToString());
                        EndlessMode_list.Add(Math.Round(EndlessMode_result / 2).ToString());
                        EndlessMode_list.Add(Math.Round(EndlessMode_result - 5).ToString());
                    }

            
                }
                riddle = EndlessMode_example;
                answers = EndlessMode_list;
                break;
            case GAME_MODE.X_3:
                (string X_3_example, double X_3_result) = X_3(MinValueWithandWithoutBrackets, MaxValueWithandWithoutBrackets, MinRangeResult, MaxRangeResult);
                List<string> X_3_list = new List<string>();
                Dictionary<double, List<string>> resultMapping_X_3 = new Dictionary<double, List<string>>
                {
                    { result1, new List<string> { result1.ToString(), result2.ToString(), result3.ToString(), result4.ToString() } },
                    { result2, new List<string> { result2.ToString(), result1.ToString(), result3.ToString(), result4.ToString() } },
                    { result3, new List<string> { result3.ToString(), result1.ToString(), result2.ToString(), result4.ToString() } },
                    { result4, new List<string> { result4.ToString(), result1.ToString(), result2.ToString(), result3.ToString() } }
                };

                if (resultMapping_X_3.ContainsKey(X_3_result))
                {
                    X_3_list.AddRange(resultMapping_X_3[X_3_result]);
                }

                else
                {
                    // Добавляем значения, округленные до целых чисел
                    if (X_3_result == 0)
                    {
                        X_3_list.Add(X_3_result.ToString());
                        X_3_list.Add(Math.Round(X_3_result + 2).ToString()); // Замена умножения на сложение 2
                        X_3_list.Add(Math.Round(X_3_result - 2).ToString()); // Замена деления на вычитание 2
                        X_3_list.Add(Math.Round(X_3_result - 5).ToString());
                    }
                    else
                    {
                        X_3_list.Add(X_3_result.ToString());
                        X_3_list.Add(Math.Round(X_3_result * 2).ToString());
                        X_3_list.Add(Math.Round(X_3_result / 2).ToString());
                        X_3_list.Add(Math.Round(X_3_result - 5).ToString());
                    }

            
                }
                riddle = X_3_example;
                answers = X_3_list;
                break;
            
            
            case GAME_MODE.FIND_X:
                (string FIND_X_example, double FIND_X_result) = GenerateEquation_Find_X(MinValue, MaxValue);
                List<string> FIND_X_List = new List<string>();
                Dictionary<double, List<string>> resultMapping_FIND_X_example = new Dictionary<double, List<string>>
                {
                    { result1, new List<string> { result1.ToString(), result2.ToString(), result3.ToString(), result4.ToString() } },
                    { result2, new List<string> { result2.ToString(), result1.ToString(), result3.ToString(), result4.ToString() } },
                    { result3, new List<string> { result3.ToString(), result1.ToString(), result2.ToString(), result4.ToString() } },
                    { result4, new List<string> { result4.ToString(), result1.ToString(), result2.ToString(), result3.ToString() } }
                };

                if (resultMapping_FIND_X_example.ContainsKey(FIND_X_result))
                {
                    FIND_X_List.AddRange(resultMapping_FIND_X_example[FIND_X_result]);
                }

                else
                {
                    // Добавляем значения, округленные до целых чисел
                    if (FIND_X_result == 0)
                    {
                        FIND_X_List.Add(FIND_X_result.ToString());
                        FIND_X_List.Add(Math.Round(FIND_X_result + 2).ToString()); // Замена умножения на сложение 2
                        FIND_X_List.Add(Math.Round(FIND_X_result - 2).ToString()); // Замена деления на вычитание 2
                        FIND_X_List.Add(Math.Round(FIND_X_result - 5).ToString());
                    }
                    else
                    {
                        FIND_X_List.Add(FIND_X_result.ToString());
                        FIND_X_List.Add(Math.Round(FIND_X_result * 2).ToString());
                        FIND_X_List.Add(Math.Round(FIND_X_result / 2).ToString());
                        FIND_X_List.Add(Math.Round(FIND_X_result - 5).ToString());
                    }

            
                }
                riddle = FIND_X_example;
                answers = FIND_X_List;
                break;
            
            
            case GAME_MODE.SUM_AND_SUBTRACT:// Work
                (string sumAndSubtractExample, double sumAndSubtractResult) = GenerateSumAndSubtractRiddle(MinValue, MaxValue);
                List<string> sumAndSubtractList = new List<string>();
                Dictionary<double, List<string>> resultMapping_sumAndSubtractExample = new Dictionary<double, List<string>>
                {
                    { result1, new List<string> { result1.ToString(), result2.ToString(), result3.ToString(), result4.ToString() } },
                    { result2, new List<string> { result2.ToString(), result1.ToString(), result3.ToString(), result4.ToString() } },
                    { result3, new List<string> { result3.ToString(), result1.ToString(), result2.ToString(), result4.ToString() } },
                    { result4, new List<string> { result4.ToString(), result1.ToString(), result2.ToString(), result3.ToString() } }
                };

                if (resultMapping_sumAndSubtractExample.ContainsKey(sumAndSubtractResult))
                {
                    sumAndSubtractList.AddRange(resultMapping_sumAndSubtractExample[sumAndSubtractResult]);
                }

                else
                {
                    // Добавляем значения, округленные до целых чисел
                    if (sumAndSubtractResult == 0)
                    {
                        sumAndSubtractList.Add(sumAndSubtractResult.ToString());
                        sumAndSubtractList.Add(Math.Round(sumAndSubtractResult + 2).ToString()); // Замена умножения на сложение 2
                        sumAndSubtractList.Add(Math.Round(sumAndSubtractResult - 2).ToString()); // Замена деления на вычитание 2
                        sumAndSubtractList.Add(Math.Round(sumAndSubtractResult - 5).ToString());
                    }
                    else
                    {
                        sumAndSubtractList.Add(sumAndSubtractResult.ToString());
                        sumAndSubtractList.Add(Math.Round(sumAndSubtractResult * 2).ToString());
                        sumAndSubtractList.Add(Math.Round(sumAndSubtractResult / 2).ToString());
                        sumAndSubtractList.Add(Math.Round(sumAndSubtractResult - 5).ToString());
                    }

            
                }
                riddle = sumAndSubtractExample;
                answers = sumAndSubtractList;
                break;
            case GAME_MODE.DOUBLE:// Work
                (string doubleExample, double doubleResult) = GenerateDoubleRiddle(MinValue,MaxValue);
                List<string> doubleList = new List<string>();
                Dictionary<double, List<string>> resultMappingDouble = new Dictionary<double, List<string>>
                {
                    { result1, new List<string> { result1.ToString(), result2.ToString(), result3.ToString(), result4.ToString() } },
                    { result2, new List<string> { result2.ToString(), result1.ToString(), result3.ToString(), result4.ToString() } },
                    { result3, new List<string> { result3.ToString(), result1.ToString(), result2.ToString(), result4.ToString() } },
                    { result4, new List<string> { result4.ToString(), result1.ToString(), result2.ToString(), result3.ToString() } }
                };

                if (resultMappingDouble.ContainsKey(doubleResult))
                {
                    doubleList.AddRange(resultMappingDouble[doubleResult]);
                }
                else
                {
                    if (doubleResult == 0)
                    {
                        doubleList.Add(doubleResult.ToString());
                        doubleList.Add(Math.Round(doubleResult + 2).ToString());
                        doubleList.Add(Math.Round(doubleResult - 2).ToString());
                        doubleList.Add(Math.Round(doubleResult - 5).ToString());
                    }
                    else
                    {
                        doubleList.Add(doubleResult.ToString());
                        doubleList.Add(Math.Round(doubleResult * 2).ToString());
                        doubleList.Add(Math.Round(doubleResult / 2).ToString());
                        doubleList.Add(Math.Round(doubleResult - 5).ToString());
                    }
                }
                riddle = doubleExample;
                answers = doubleList;
                break;
            case GAME_MODE.MULTIPLICATION_AND_DIVISION:
                (string multiplicationAndDivisionExample, double multiplicationAndDivisionResult) = GenerateMultiplicationAndDivisionRiddle(MinValue, MaxValue);
                List<string> multiplicationAndDivisionList = new List<string>();
                Dictionary<double, List<string>> resultMappingMultiplicationAndDivision = new Dictionary<double, List<string>>
                {
                    { result1, new List<string> { result1.ToString(), result2.ToString(), result3.ToString(), result4.ToString() } },
                    { result2, new List<string> { result2.ToString(), result1.ToString(), result3.ToString(), result4.ToString() } },
                    { result3, new List<string> { result3.ToString(), result1.ToString(), result2.ToString(), result4.ToString() } },
                    { result4, new List<string> { result4.ToString(), result1.ToString(), result2.ToString(), result3.ToString() } }
                };

                if (resultMappingMultiplicationAndDivision.ContainsKey(multiplicationAndDivisionResult))
                { 
                    multiplicationAndDivisionList.AddRange(resultMappingMultiplicationAndDivision[multiplicationAndDivisionResult]);
                }
                else
                {
                if (multiplicationAndDivisionResult == 0)
                {
                    multiplicationAndDivisionList.Add(multiplicationAndDivisionResult.ToString());
                    multiplicationAndDivisionList.Add(Math.Round(multiplicationAndDivisionResult + 2).ToString());
                    multiplicationAndDivisionList.Add(Math.Round(multiplicationAndDivisionResult - 2).ToString());
                    multiplicationAndDivisionList.Add(Math.Round(multiplicationAndDivisionResult - 5).ToString());
                }
                else
                {
                    multiplicationAndDivisionList.Add(multiplicationAndDivisionResult.ToString());
                    multiplicationAndDivisionList.Add(Math.Round(multiplicationAndDivisionResult * 2).ToString());
                    multiplicationAndDivisionList.Add(Math.Round(multiplicationAndDivisionResult / 2).ToString());
                    multiplicationAndDivisionList.Add(Math.Round(multiplicationAndDivisionResult - 5).ToString());
                }
                }
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
            if (shuffledCurrentAnswers[i] == currentRiddle.GetAnswers[0]) return i;
        
        return 10;
    }

    void Timer()
    {
        if(!useTimer) return;
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
