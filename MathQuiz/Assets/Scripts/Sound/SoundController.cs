using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    private AudioSource audioSource;

    [SerializeField] private AudioClip buttonClickSound;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Destroying duplicate SoundController object - only one is allowed per scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = PlayerPrefs.GetInt("soundEnable") == 0;
        GameAction.soundEnable += SoundActivator;
    }

    private void OnDestroy()
    {
        GameAction.soundEnable -= SoundActivator;
    }

    void SoundActivator(bool active) => audioSource.enabled = active;

    public void PlayButtonClickSound() => audioSource.PlayOneShot(buttonClickSound);
    //public void PlayCorrectAnsweredSound() => audioSource.PlayOneShot(buttonClickSound);
    //public void PlayGameOverSound() => audioSource.PlayOneShot(buttonClickSound);
}
