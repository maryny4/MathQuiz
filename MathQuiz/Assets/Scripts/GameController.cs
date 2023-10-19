using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private Riddle currentRiddle;
    
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

    void GenerateRiddle()
    {
        currentRiddle = new Riddle();
        currentRiddle.SetRiddle("1+1");
        currentRiddle.SetAnswers(Answers);//0 correct, 1-2-3 wrong
        
        
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
