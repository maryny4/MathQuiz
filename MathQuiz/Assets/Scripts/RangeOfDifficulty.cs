using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RangeOfDifficulty : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rangeText;
    private Slider slider;
    private int minValue = 15;
    private int maxValue = 100;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
        slider.value = Mathf.InverseLerp(minValue, maxValue, Globals.instance.GetRangeOfDifficulty.Item2);
        OnValueChanged();
    }
    void OnValueChanged()
    {
        float normalizedValue = slider.value;
        int newRange = (int)Mathf.Lerp(minValue, maxValue, normalizedValue);
        Globals.instance.SetRangeOfDifficulty(newRange);
        rangeText.text = (-newRange).ToString() + " / " + newRange.ToString();
    }
}
