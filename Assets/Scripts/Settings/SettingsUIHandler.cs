using System.Collections.Generic;
using UnityEngine;
using CharacterControl.Manager;

namespace CharacterControl.MainMenu
{
    public class SettingsUIHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private List<Canvas> _canvasesToDisable;
        [SerializeField] private List<Canvas> _canvasesToEnableWhenClosing;
        [SerializeField] private List<GameObject> _rightSidePanels;
        [SerializeField] private Canvas _pauseMenuCanvas;
        [SerializeField] private InputManager _inputManager;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip _pauseSoundClip;
        [SerializeField] private AudioClip _resumeSoundClip;
        [SerializeField] private AudioSource _uiAudioSource;

        private bool _isPaused = false;
        private bool _pauseButtonPreviouslyPressed = false;

        private void Start()
        {
            ForceCloseCurrent();
            CloseAllRightPanels();
            if (_pauseMenuCanvas != null)
                _pauseMenuCanvas.gameObject.SetActive(false);
        }

        private void Update()
        {
            HandlePauseInput();
        }

        private void HandlePauseInput()
        {
            if (_inputManager.PauseAction)
            {
                if (!_pauseButtonPreviouslyPressed)
                {
                    if (_isPaused)
                        ResumeGame();
                    else
                        PauseGame();

                    _pauseButtonPreviouslyPressed = true;
                }
            }
            else
            {
                _pauseButtonPreviouslyPressed = false;
            }
        }

        private void PauseGame()
        {
            if (_pauseMenuCanvas != null)
                GlobalUIManager.RequestOpen(_pauseMenuCanvas, OnPauseMenuClosed);

            Time.timeScale = 0f;
            _isPaused = true;

            PlayUISound(_pauseSoundClip);
            Debug.Log("[SettingsUI] Game Paused.");
        }

        private void ResumeGame()
        {

            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _isPaused = false;

            PlayUISound(_resumeSoundClip);

            if (_pauseMenuCanvas != null)
                _pauseMenuCanvas.gameObject.SetActive(false);

            Debug.Log("[SettingsUI] Game Resumed.");
        }

        private void OnPauseMenuClosed()
        {
            Time.timeScale = 1f;
            _isPaused = false;
            AudioListener.pause = false;
            Debug.Log("[SettingsUI] Pause Menu closed manually, game resumed.");
        }


        private void PlayUISound(AudioClip clip)
        {
            if (_uiAudioSource != null && clip != null)
            {
                _uiAudioSource.PlayOneShot(clip);
            }
        }

        private void ForceCloseCurrent()
        {
            foreach (Canvas c in _canvasesToDisable)
            {
                if (c != null)
                    c.gameObject.SetActive(false);
            }
        }

        private void ForceEnableDefaults()
        {
            foreach (Canvas c in _canvasesToEnableWhenClosing)
            {
                if (c != null)
                    c.gameObject.SetActive(true);
            }
        }

        private void CloseAllRightPanels()
        {
            foreach (var panel in _rightSidePanels)
            {
                if (panel != null)
                    panel.SetActive(false);
            }
        }

        public void OpenRightPanel1()
        {
            CloseAllRightPanels();
            if (_rightSidePanels.Count > 0 && _rightSidePanels[0] != null)
                _rightSidePanels[0].SetActive(true);
        }

        public void OpenRightPanel2()
        {
            CloseAllRightPanels();
            if (_rightSidePanels.Count > 1 && _rightSidePanels[1] != null)
                _rightSidePanels[1].SetActive(true);
        }

        public void OpenRightPanel3()
        {
            CloseAllRightPanels();
            if (_rightSidePanels.Count > 2 && _rightSidePanels[2] != null)
                _rightSidePanels[2].SetActive(true);
        }

        public void OpenRightPanel4()
        {
            CloseAllRightPanels();
            if (_rightSidePanels.Count > 3 && _rightSidePanels[3] != null)
                _rightSidePanels[3].SetActive(true);
        }

        public void OpenRightPanel5()
        {
            CloseAllRightPanels();
            if (_rightSidePanels.Count > 4 && _rightSidePanels[4] != null)
                _rightSidePanels[4].SetActive(true);
        }

        public void CloseAllPanels()
        {
            ForceCloseCurrent();
            ForceEnableDefaults();
            CloseAllRightPanels();
            GlobalUIManager.ForceCloseCurrent();
        }
    }
}
