using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private GAME_MODE gameMode;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => StartGame());
    }
    private void StartGame()
    {
        Globals.instance.SetCurrentGameMode(gameMode);
        SoundController.instance.PlayButtonClickSound();
        StartCoroutine(SceneLoader.LoadScene(2));
    }
}
