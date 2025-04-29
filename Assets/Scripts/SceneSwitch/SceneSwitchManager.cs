using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using CharacterControl.Settings;

namespace SceneSwitchManagerNameSpace.SceneManagement
{
    public class SceneSwitchManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator _globalCanvasAnimator;
        [SerializeField] private float _fadeDuration = 1.5f;

        [Header("Scene Names")]
        [SerializeField] private string _houseSceneName = "House";
        [SerializeField] private string _forestSceneName = "ForestMap";

        [Header("Spawn Positions")]
        [SerializeField] private Vector3 _houseSpawnPosition = new Vector3(-1.76395464f, 0.121195674f, 3.2233901f);
        [SerializeField] private Vector3 _forestSpawnPosition = new Vector3(296.167999f, 4.95300007f, 392.963989f);

        private Scene _houseScene;
        private Scene _forestScene;

        private readonly string[] _specialCanvases = { "PausedCanvas", "StoryCanvas", "MapCanvas", "MemoryCanvas" };
        private bool _isSwitching = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SwitchHouseToForest()
        {
            if (_isSwitching) return;
            StartCoroutine(SwitchScenes(_houseSceneName, _forestSceneName));
        }

        public void SwitchForestToHouse()
        {
            if (_isSwitching) return;
            StartCoroutine(SwitchScenes(_forestSceneName, _houseSceneName));
        }

        private IEnumerator SwitchScenes(string fromSceneName, string toSceneName)
        {
            _isSwitching = true;

            if (_globalCanvasAnimator != null)
            {
                _globalCanvasAnimator.SetTrigger("FadeInGlobalParam");
            }
            else
            {
                Debug.LogWarning("GlobalCanvas Animator not assigned!");
            }

            yield return new WaitForSeconds(_fadeDuration);

            if (!SceneManager.GetSceneByName(toSceneName).isLoaded)
            {
                AsyncOperation loadOperation = SceneManager.LoadSceneAsync(toSceneName, LoadSceneMode.Additive);
                while (!loadOperation.isDone)
                    yield return null;
            }

            _houseScene = SceneManager.GetSceneByName(_houseSceneName);
            _forestScene = SceneManager.GetSceneByName(_forestSceneName);

            Scene toScene = SceneManager.GetSceneByName(toSceneName);

            if (toScene.isLoaded)
            {
                SceneManager.SetActiveScene(toScene);

                foreach (GameObject obj in toScene.GetRootGameObjects())
                {
                    if (IsSpecialCanvas(obj))
                    {
                        obj.SetActive(false);
                    }
                    else
                    {
                        obj.SetActive(true);
                    }
                }

            }

            if (fromSceneName == _forestSceneName && toSceneName == _houseSceneName)
            {
                var timelineManager = GameObject.Find("TimelineManager");
                if (timelineManager != null)
                {
                    timelineManager.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("TimelineManager not found when switching Forest to House.");
                }
            }

            if (!SettingsDatabase.CurrentSettings.SubtitlesEnabled)
            {
                Debug.Log("Subtitles disabled, adjust UI accordingly.");
            }

            Scene fromScene = SceneManager.GetSceneByName(fromSceneName);

            if (fromScene.isLoaded)
            {
                foreach (GameObject obj in fromScene.GetRootGameObjects())
                {
                    obj.SetActive(false);
                }
            }

            if (_globalCanvasAnimator != null)
            {
                _globalCanvasAnimator.SetTrigger("FadeOutGlobalParam");
            }

            yield return new WaitForSeconds(_fadeDuration);

            _isSwitching = false;
        }

        private bool IsSpecialCanvas(GameObject obj)
        {
            foreach (string name in _specialCanvases)
            {
                if (obj.name == name)
                    return true;
            }
            return false;
        }
    }
}