using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoaderScript : MonoBehaviour
{
    [Header("UI/Animation")]
    [SerializeField] private Animator CanvasAnimator;

    [Header("Audio")]
    [SerializeField] private AudioSource ThemeSource;

    [Header("Timing")]
    [SerializeField] private float WaitForSplash = 16f;

    private AsyncOperation asyncOperation;
    private bool isLoaded = false;
    private bool isPressed = false;
    private bool readyToContinue = false;

    void Awake()
    {
        StartCoroutine(LoadSceneAsync("Main Menu"));
        StartCoroutine(SplashSequence());
    }

    void LoadScene()
    {
        asyncOperation.allowSceneActivation = true;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!isLoaded)
        {
            if (asyncOperation.progress >= 0.9f)
                isLoaded = true;

            yield return null;
        }

        yield return new WaitUntil(() => readyToContinue);
        Debug.Log("Press any key to continue...");

        yield return new WaitUntil(() => Input.anyKey && !isPressed);

        isPressed = true;

        if (CanvasAnimator != null)
            CanvasAnimator.SetTrigger("LoadMainMenuParam");
            for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    ThemeSource.volume = i;
                    yield return null;
                }
            
        
        Invoke("LoadScene", 1.5f);
    }

    IEnumerator SplashSequence()
    {
        yield return new WaitForSeconds(WaitForSplash);
        readyToContinue = true;

        yield return null;
    }

}
