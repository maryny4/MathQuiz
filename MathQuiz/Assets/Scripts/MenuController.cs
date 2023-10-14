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
        startGameButton.onClick.AddListener(() => StartCoroutine(StartGame()));
    }

    IEnumerator StartGame()
    {
        asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = false;
        
        //TODO show transition screen
        yield return new WaitForSeconds(.5f);
        asyncLoad.allowSceneActivation = true;
    }
}
