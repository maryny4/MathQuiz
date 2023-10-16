using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : PageAnimation
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMenuButton;

    private void Start()
    {
        restartButton.onClick.AddListener(() => RestartGame());
        backToMenuButton.onClick.AddListener(() => StartCoroutine(SceneLoader.LoadScene(1)));
        base.Start();
    }

    public void ShowPanel(int score)
    {
        int bestScore = Globals.instance.GetBestScore(score);
        
        scoreText.text = "SCORE: " + score;
        bestScoreText.text = "BEST SCORE: " + bestScore;
        
        base.ShowPanel();
    }

    void RestartGame()
    {
        GameAction.startGame?.Invoke(true);
        HidePanel();
    }
}
