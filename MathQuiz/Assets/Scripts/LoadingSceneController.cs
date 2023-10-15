using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private CanvasGroup splashScreen;
    IEnumerator Start()
    {
        splashScreen.transform.DOScale(1.2f, 2.5f);
        splashScreen.DOFade(0, 1.5f).SetDelay(1);
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(SceneLoader.LoadScene(1));
    }
}
