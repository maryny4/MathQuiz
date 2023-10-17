using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private AsyncOperation asyncLoad;

    [SerializeField] private Button startGameButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(() => StartGame());
    }

    void StartGame()
    {
        SoundController.instance.PlayButtonClickSound();
        StartCoroutine(SceneLoader.LoadScene(2));
    }
}
