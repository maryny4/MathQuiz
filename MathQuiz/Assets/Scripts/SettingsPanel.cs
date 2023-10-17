using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : PageAnimation
{
    //todo dodać system trudności

    [SerializeField] private Button openSettingsButton;
    [SerializeField] private Button closeSettingsButton;
    public void Start()
    {
        base.Start();
        openSettingsButton.onClick.AddListener(() => ShowPanel());
        closeSettingsButton.onClick.AddListener(() => HidePanel());
    }

    void ShowPanel()
    {
        base.ShowPanel();
        SoundController.instance.PlayButtonClickSound();
    }
    void HidePanel()
    {
        base.HidePanel();
        SoundController.instance.PlayButtonClickSound();
    }
}
