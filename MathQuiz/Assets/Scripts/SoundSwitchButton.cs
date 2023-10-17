using UnityEngine;
using UnityEngine.UI;
public class SoundSwitchButton : MonoBehaviour
{
    [SerializeField] private Image soundIcon;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    private Button button;
    private bool soundEnable;
    private void Start()
    {
        soundEnable = PlayerPrefs.GetInt("soundEnable") == 0;
        button = GetComponent<Button>();
        button.onClick.AddListener(SoundSwitch);
        SetButton();
    }

    void SetButton()
    {
        soundIcon.sprite = soundEnable ? soundOn : soundOff;
        button.image.color = new Color(button.image.color.r, button.image.color.g, button.image.color.b,
            soundEnable ? 1 : .7f);
    }

    public void SoundSwitch()
    {
        soundEnable = !soundEnable;
        PlayerPrefs.SetInt("soundEnable", soundEnable ? 0 : 1);
        GameAction.soundEnable?.Invoke(soundEnable);
        SoundController.instance.PlayButtonClickSound();
        SetButton();
    }
}