using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLButton : MonoBehaviour
{
    [SerializeField] private string URL = "https://www.google.com";
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => OpenURL());
    }

    void OpenURL()
    {
        SoundController.instance.PlayButtonClickSound();
        Application.OpenURL(URL);
    }
}
