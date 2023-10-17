using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : PageAnimation
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMenuButton;

    private void Start()
    {
        restartButton.onClick.AddListener(() => RestartGame());
        backToMenuButton.onClick.AddListener(() => OpenMenu());
        base.Start();
    }

    public void ShowPanelWithDelay(int score, string title, float delay = 1)
    {
        StartCoroutine(ShowPanel(score, title, delay));
    }

    IEnumerator ShowPanel(int score, string title, float delay)
    {
        yield return new WaitForSeconds(delay);
        int bestScore = Globals.instance.GetBestScore(score);
        titleText.text = title;
        scoreText.text = "SCORE: " + score;
        bestScoreText.text = "BEST SCORE: " + bestScore;
        base.ShowPanel();
    }

    void RestartGame()
    {
        SoundController.instance.PlayButtonClickSound();
        GameAction.startGame?.Invoke(true);
        HidePanel();
    }

    void OpenMenu()
    {
        SoundController.instance.PlayButtonClickSound();
        StartCoroutine(SceneLoader.LoadScene(1));
    }
}
