using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private float animDuration = .4f;
    [SerializeField] private CanvasGroup background;
    [SerializeField] private Transform[] animatedElements;
    [SerializeField] private Ease scaleEasy;
    [SerializeField] private List<Vector3> elementsScale;

    private void Start()
    {
        restartButton.onClick.AddListener(() => RestartGame());
        backToMenuButton.onClick.AddListener(() => StartCoroutine(SceneLoader.LoadScene(1)));

        background.alpha = 0;
        background.blocksRaycasts = false;
        for (int i = 0; i < animatedElements.Length; i++)
            elementsScale.Add(animatedElements[i].localScale);
    }

    public void ShowPanel(int score)
    {
        scoreText.text = "SCORE: " + score;
        bestScoreText.text = "BEST SCORE: " + Globals.instance.GetBestScore(score);
        background.blocksRaycasts = true;
        background.DOFade(1, animDuration);
        for (int i = 0; i < animatedElements.Length; i++)
            animatedElements[i].DOScale(elementsScale[i], animDuration).SetEase(scaleEasy);
    }

    void RestartGame()
    {
        GameAction.startGame?.Invoke(true);
        HidePanel();
    }

    void HidePanel()
    {
        background.blocksRaycasts = false;
        background.DOFade(0, animDuration);
        foreach (var element in animatedElements)
            element.DOScale(Vector3.zero, animDuration).SetEase(scaleEasy);
    }
}
