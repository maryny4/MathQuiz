using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private CanvasGroup splashScreen;
    IEnumerator Start()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene");
        asyncLoad.allowSceneActivation = false;
        
        splashScreen.transform.DOScale(1.2f, 2.5f);
        splashScreen.DOFade(0, 1.5f).SetDelay(1);
        
        yield return new WaitForSeconds(2.5f);
        //TODO show transition screen
        asyncLoad.allowSceneActivation = true;
    }
}
