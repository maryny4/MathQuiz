using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RangeOfDifficulty : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private Slider slider;
    [SerializeField] private Button negativeButton;
    [SerializeField] private TextMeshProUGUI negativeButtonText;
    private int minValue = 15;
    private int maxValue = 100;
    private int currentRange;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { SliderUpdate(); });
        slider.value = Mathf.InverseLerp(minValue, maxValue, Globals.instance.GetRangeOfDifficulty);
        SliderUpdate();
        
        negativeButton.onClick.AddListener(() => SwitchNegativeNumbers());
        negativeButtonText.text = RangeTypeText();
    }
    void SliderUpdate()
    {
        float normalizedValue = slider.value;
        currentRange = (int)Mathf.Lerp(minValue, maxValue , normalizedValue);
        Globals.instance.SetRangeOfDifficulty(currentRange);
        rangeText.text = RangeText();
    }

    void SwitchNegativeNumbers()
    {
        SoundController.instance.PlayButtonClickSound();
        Globals.instance.NegativeRangeSwitcher();
        StartCoroutine(TextUpdater.UpdateText(negativeButtonText, RangeTypeText()));
        StartCoroutine(TextUpdater.UpdateText(rangeText, RangeText()));
    }

    private string RangeText() 
    {
        return Globals.instance.NegativeRangeState ? (-currentRange + " / " + currentRange) : ("0 / " + currentRange);
    }

    private string RangeTypeText() => Globals.instance.NegativeRangeState ? "-X / X" : "X";
}
