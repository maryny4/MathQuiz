using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCountText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    void Start()
    {
        coinCountText.text = PlayerPrefs.GetInt("COINS") + "<sprite=0>";
        bestScoreText.text = "BEST SCORE: " + Globals.instance.GetBestScore(0);
    }
}
