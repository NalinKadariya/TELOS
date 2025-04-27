using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CharacterControl.MainMenu
{
    public class MainMenuButtonsScript : MonoBehaviour
    {
        [SerializeField] private Animator canvasAnimator;
        [SerializeField] private Animator mainMenuAnimator;
        [SerializeField] private string sceneToLoad = "House";
        [SerializeField] private float fadeOutDuration = 1.5f;

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit Game");
        }

        public void PlayGame()
        {
            Debug.Log("Play Game");
            if (canvasAnimator != null)
            {
                mainMenuAnimator.SetTrigger("HideMenu");
                canvasAnimator.SetTrigger("FadeOutSceneParam");
                StartCoroutine(LoadSceneAfterFade());
            }
            else
            {
                Debug.LogWarning("Canvas Animator not assigned!");
            }
        }

        private IEnumerator LoadSceneAfterFade()
        {
            yield return new WaitForSeconds(fadeOutDuration);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
