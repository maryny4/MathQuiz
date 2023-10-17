using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PageAnimation : MonoBehaviour
{
    [SerializeField] private float animDuration = .3f;
    [SerializeField] private CanvasGroup background;
    [SerializeField] private Transform[] animatedElements;
    [SerializeField] private Ease scaleEasy;
    [SerializeField] private List<Vector3> elementsScale;
    private Vector3 hiddenScale = new Vector3(.5f, .5f, .5f);

    public void Start()
    {
        background.alpha = 0;
        background.blocksRaycasts = false;
        for (int i = 0; i < animatedElements.Length; i++)
        {
            elementsScale.Add(animatedElements[i].localScale);
            animatedElements[i].localScale = hiddenScale;
        }
    }

    public void ShowPanel()
    {
        background.blocksRaycasts = true;
        background.DOFade(1, animDuration);
        for (int i = 0; i < animatedElements.Length; i++)
            animatedElements[i].DOScale(elementsScale[i], animDuration).SetEase(scaleEasy);
    }

    public void HidePanel()
    {
        background.blocksRaycasts = false;
        background.DOFade(0, animDuration);
        foreach (var element in animatedElements)
            element.DOScale(hiddenScale, animDuration).SetEase(scaleEasy);
    }
}