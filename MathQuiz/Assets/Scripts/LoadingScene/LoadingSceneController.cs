using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private CanvasGroup splashScreen;
    IEnumerator Start()
    {
        GameAnalytics.Initialize();
        splashScreen.transform.DOScale(1.2f, 2.5f);
        splashScreen.DOFade(0, 1.5f).SetDelay(1);
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(SceneLoader.LoadScene(1));
    }
}
