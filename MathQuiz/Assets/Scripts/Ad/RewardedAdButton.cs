using UnityEngine;
using UnityEngine.UI;

public class RewardedAdButton : MonoBehaviour
{
    [SerializeField] private GameOverPanel gameOverPanel;
    public void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(()=> RewardedAdController.instance.ShowRewardedAd(this));
    }

    public virtual void GetReward()
    {
        Globals.instance.X2_Coin();
        gameOverPanel.Used_2x_Reward();
        Debug.Log("GetReward");
    }
}
