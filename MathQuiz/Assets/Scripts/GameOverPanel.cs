using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : PageAnimation
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI rewardCoinCountText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button _2xRewardButton;
    [SerializeField] private TextMeshProUGUI coinsText;
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
        rewardCoinCountText.text = "+" + Globals.instance.RewardCoins + " <sprite=0>";
        _2xRewardButton.gameObject.SetActive(Globals.instance.RewardCoins > 0);
        base.ShowPanel();
    }

    public void Used_2x_Reward()
    {
        _2xRewardButton.gameObject.SetActive(false);
        rewardCoinCountText.text = "+" + Globals.instance.RewardCoins + " <sprite=0>";
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
