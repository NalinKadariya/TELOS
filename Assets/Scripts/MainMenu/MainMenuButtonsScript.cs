using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CharacterControl.MainMenu
{
    public class MainMenuButtonsScript : MonoBehaviour
    {
        [SerializeField] private Animator canvasAnimator;
        [SerializeField] private Animator mainMenuAnimator;
        [SerializeField] private string houseSceneName = "House";
        [SerializeField] private string forestSceneName = "ForestMap";
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
                StartCoroutine(LoadScenesAfterFade());
            }
            else
            {
                Debug.LogWarning("Canvas Animator not assigned!");
            }
        }

        private IEnumerator LoadScenesAfterFade()
        {
            yield return new WaitForSeconds(fadeOutDuration);

            AsyncOperation houseLoad = SceneManager.LoadSceneAsync(houseSceneName, LoadSceneMode.Additive);
            AsyncOperation forestLoad = SceneManager.LoadSceneAsync(forestSceneName, LoadSceneMode.Additive);

            houseLoad.allowSceneActivation = false;
            forestLoad.allowSceneActivation = false;

            while (!houseLoad.isDone || !forestLoad.isDone)
            {
                if (houseLoad.progress >= 0.9f)
                    houseLoad.allowSceneActivation = true;

                if (forestLoad.progress >= 0.9f)
                    forestLoad.allowSceneActivation = true;

                yield return null;
            }

            Scene forestScene = SceneManager.GetSceneByName(forestSceneName);
            Scene houseScene = SceneManager.GetSceneByName(houseSceneName);

            if (forestScene.isLoaded)
            {
                foreach (GameObject obj in forestScene.GetRootGameObjects())
                {
                    obj.SetActive(false); 
                }
            }
            else
            {
                Debug.LogError("Forest scene not loaded properly!");
            }

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(currentScene);
        
        }
    }
}
