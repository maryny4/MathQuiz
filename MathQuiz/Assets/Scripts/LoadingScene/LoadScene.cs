using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{ 
    public static IEnumerator LoadScene(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;
        GameAction.showTransitionScreen?.Invoke();
        yield return new WaitForSeconds(Globals.instance.transitionDelay + Globals.instance.transitionDuration);
        asyncLoad.allowSceneActivation = true;
    }
}
