using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button hintButton;
    
    private Riddle currentRiddle;
    private List<string> shuffledCurrentAnswers;
    void Start()
    {
        GameAction.onClickAnswer += CheckAnswer;
    }

    private void OnDestroy()
    {
        GameAction.onClickAnswer -= CheckAnswer;
    }

    void CheckAnswer(int answerIndex)
    {
        if (shuffledCurrentAnswers[answerIndex] == currentRiddle.GetAnswers[0]) AnsweredCorrectly();
        else AnsweredWrongly();
    }

    void GenerateRiddle()
    {
        currentRiddle = Globals.instance.riddleDataList.GetRandomRiddle();
        shuffledCurrentAnswers = new List<string>(currentRiddle.GetAnswers);
        ListShuffler.Shuffle(shuffledCurrentAnswers);
    }

    void AnsweredCorrectly()
    {
        
    }

    void AnsweredWrongly()
    {
        
    }
}
