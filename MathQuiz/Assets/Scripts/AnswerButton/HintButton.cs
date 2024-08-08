using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private HINT_TYPE hint;
    private Color defaultColor;
    
    [SerializeField] private Color disabledColor;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => UseHint());
        defaultColor = button.image.color;
        GameAction.resetHints += ResetHint;
        GameAction.disableHint += DisableHint;
    }

    private void OnDestroy()
    {
        GameAction.resetHints -= ResetHint;
        GameAction.disableHint -= DisableHint;
    }

    void UseHint()
    {
        SoundController.instance.PlayButtonClickSound();
        GameAction.useHint?.Invoke(hint);
    }

    void ResetHint()
    {
        button.image.color = defaultColor;
        button.interactable = true;
    }

    void DisableHint(HINT_TYPE hintType)
    {
        if(hint != hintType) return;
        
        button.image.color = disabledColor;
        button.interactable = false;
    }
}
