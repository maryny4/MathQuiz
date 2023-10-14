using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{
    public static Globals instance;

    public RiddleDataList riddleDataList;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Destroying duplicate Globals object - only one is allowed per scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}

public static class GameAction
{
    public static Action<int> onClickAnswer;
    public static Action generateQuestion;
}