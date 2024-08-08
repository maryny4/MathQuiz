using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup background;
    [SerializeField] private float hidePosX = 2620;
    [SerializeField] private Ease easeShow;
    [SerializeField] private Ease easeHide;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) HideTransition();
        GameAction.showTransitionScreen += ShowTransition;
    }
    private void OnDestroy()
    {
        GameAction.showTransitionScreen -= ShowTransition;
    }

    [ContextMenu("ShowTransition")]
    public void ShowTransition()
    {
        background.transform.localPosition = new Vector3(hidePosX, 0, 0);
        background.transform.DOLocalMoveX(0, Globals.instance.transitionDuration).SetEase(easeShow);
    }
    [ContextMenu("HideTransition")]
    void HideTransition()
    {
        background.transform.localPosition = Vector3.zero;
        background.transform.DOLocalMoveX(-hidePosX, Globals.instance.transitionDuration).SetEase(easeHide)
            .SetDelay(Globals.instance.transitionDelay);
    }
}