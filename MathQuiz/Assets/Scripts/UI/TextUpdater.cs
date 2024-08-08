using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public static class TextUpdater
{
    public static IEnumerator UpdateText(TextMeshProUGUI text, string value, float delay = .4f, Ease ease = Ease.Unset)
    {
        text.transform.DOScale(Vector3.zero, delay / 2).SetEase(ease);
        yield return new WaitForSeconds(delay / 2);
        text.text = value;
        text.transform.DOScale(Vector3.one, delay / 2).SetEase(ease);
    }
}
