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
    [SerializeField] private Color disabledColor;
    private int buttonIndex;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClickAnswer());
        buttonIndex = transform.GetSiblingIndex();
        
        GameAction.setAnswers += SetAnswer;
        GameAction.setButtonsColor += SetButtonColor;
        GameAction.timeIsOver += SetWrongColor;
        GameAction.disableHalfAnswers += DisableHalfButtons;
        GameAction.showCorrectAnswer += DisableWrongButtons;
        
        button.image.color = defaultColor;
    }

    private void OnDestroy()
    {
        GameAction.setAnswers -= SetAnswer;
        GameAction.setButtonsColor -= SetButtonColor;
        GameAction.timeIsOver -= SetWrongColor;
        GameAction.disableHalfAnswers -= DisableHalfButtons;
        GameAction.showCorrectAnswer -= DisableWrongButtons;
    }

    void OnClickAnswer()
    {
        GameAction.onClickAnswer?.Invoke(buttonIndex);
        SoundController.instance.PlayButtonClickSound();
    }

    void SetAnswer(List<string> answers)
    {
        StartCoroutine(TextUpdater.UpdateText(answerText, answers[buttonIndex]));
        button.image.color = defaultColor;
        button.interactable = true;
    }

    void SetButtonColor(int correctAnswer, int wrongAnswer)
    {
        if (correctAnswer == buttonIndex) button.image.color = correctColor;
        else if (wrongAnswer == buttonIndex) button.image.color = wrongColor;
    }

    private void SetWrongColor() => button.image.color = wrongColor;

    private void DisableHalfButtons(int firstWrongAnswer, int secondWrongAnswer)
    {
        if (buttonIndex == firstWrongAnswer || buttonIndex == secondWrongAnswer)
        {
            button.image.color = disabledColor;
            button.interactable = false;
        }
    }
    
    private void DisableWrongButtons(int correctButton)
    {
        if (buttonIndex == correctButton) return;
        button.image.color = disabledColor;
        button.interactable = false;
    }
}
