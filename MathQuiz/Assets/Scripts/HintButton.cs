using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private HINT_TYPE hint;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => UseHint());
    }

    void UseHint()
    {
        SoundController.instance.PlayButtonClickSound();
        GameAction.useHint?.Invoke(hint);
    }
}
