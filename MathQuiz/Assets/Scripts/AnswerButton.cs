using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClickAnswer());
        GameAction.setAnswers += SetAnswer;
        GameAction.setButtonsColor += SetButtonColor;
        GameAction.timeIsOver += SetWrongColor;
        
        button.image.color = defaultColor;
    }

    private void OnDestroy()
    {
        GameAction.setAnswers -= SetAnswer;
        GameAction.setButtonsColor -= SetButtonColor;
        GameAction.timeIsOver -= SetWrongColor;
    }

    void OnClickAnswer()
    {
        GameAction.onClickAnswer?.Invoke(transform.GetSiblingIndex());
        SoundController.instance.PlayButtonClickSound();
    }

    void SetAnswer(List<string> answers)
    {
        StartCoroutine(TextUpdater.UpdateText(answerText, answers[transform.GetSiblingIndex()]));
        button.image.color = defaultColor;
    }

    void SetButtonColor(int correctAnswer, int wrongAnswer)
    {
        if (correctAnswer == transform.GetSiblingIndex()) button.image.color = correctColor;
        else if (wrongAnswer == transform.GetSiblingIndex()) button.image.color = wrongColor;
    }

    private void SetWrongColor() => button.image.color = wrongColor;
}
