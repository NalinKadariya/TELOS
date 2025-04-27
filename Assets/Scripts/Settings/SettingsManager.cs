using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using CharacterControl.Settings;
using CharacterControl.PlayerControl;

namespace CharacterControl.MainMenu
{
    public class SettingsManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioMixer _masterMixer;
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _mouseSensitivitySlider;
        [SerializeField] private TMP_Dropdown _graphicsDropdown;
        [SerializeField] private TMP_Dropdown _subtitlesDropdown;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameObject _subtitlesCanvas;

        private void Start()
        {
            ApplySettingsToUI();
            ApplySettingsToGame();
        }

        public void ApplySettingsToUI()
        {
            var settings = SettingsDatabase.CurrentSettings;

            if (_masterVolumeSlider != null)
            {
                _masterVolumeSlider.SetValueWithoutNotify(settings.MasterVolume);
                Debug.Log("[Settings] Master Volume set to: " + settings.MasterVolume);
            }

            if (_mouseSensitivitySlider != null)
            {
                _mouseSensitivitySlider.SetValueWithoutNotify(settings.MouseSensitivity);
                Debug.Log("[Settings] Mouse Sensitivity set to: " + settings.MouseSensitivity);
            }

            if (_graphicsDropdown != null)
            {
                _graphicsDropdown.SetValueWithoutNotify(settings.GraphicsQuality);
                Debug.Log("[Settings] Graphics Quality set to: " + settings.GraphicsQuality);
            }

            if (_subtitlesDropdown != null)
            {
                _subtitlesDropdown.SetValueWithoutNotify(settings.SubtitlesEnabled ? 0 : 1);
                Debug.Log("[Settings] Subtitles set to: " + (settings.SubtitlesEnabled ? "Enabled" : "Disabled"));
            }
        }

        public void ApplySettingsToGame()
        {
            var settings = SettingsDatabase.CurrentSettings;

            SetMasterVolume(settings.MasterVolume);
            SetMouseSensitivity(settings.MouseSensitivity);
            SetGraphicsQuality(settings.GraphicsQuality);
            SetSubtitles(settings.SubtitlesEnabled);
        }

        public void OnMasterVolumeChanged(float value)
        {
            SettingsDatabase.CurrentSettings.MasterVolume = value;
            SetMasterVolume(value);
            Debug.Log("[Settings] Master Volume changed to: " + value);
        }

        public void OnMouseSensitivityChanged(float value)
        {
            SettingsDatabase.CurrentSettings.MouseSensitivity = value;
            SetMouseSensitivity(value);
            Debug.Log("[Settings] Mouse Sensitivity changed to: " + value);
        }

        public void OnGraphicsQualityChanged(int index)
        {
            SettingsDatabase.CurrentSettings.GraphicsQuality = index;
            SetGraphicsQuality(index);
            Debug.Log("[Settings] Graphics Quality changed to: " + index);
        }

        public void OnSubtitlesChanged(int index)
        {
            bool enabled = (index == 0);
            SettingsDatabase.CurrentSettings.SubtitlesEnabled = enabled;
            SetSubtitles(enabled);
            Debug.Log("[Settings] Subtitles changed to: " + (enabled ? "Enabled" : "Disabled"));
        }

        public void ResetToDefault()
        {
            SettingsDatabase.ResetToDefault();
            ApplySettingsToUI();
            ApplySettingsToGame();
            Debug.Log("[Settings] Reset to default settings.");
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("[Settings] Game is quitting.");
        }

        private void SetMasterVolume(float volume)
        {
            _masterMixer.SetFloat("MasterVolumeParam", volume);
        }

        private void SetMouseSensitivity(float sensitivity)
        {
            if (_playerController != null)
            {
                _playerController.SetMouseSensitivity(sensitivity);
            }
            else
            {
                Debug.LogWarning("[Settings] PlayerController not assigned.");
            }
        }

        private void SetGraphicsQuality(int qualityLevel)
        {
            QualitySettings.SetQualityLevel(qualityLevel);
        }

        private void SetSubtitles(bool enabled)
        {
            if (_subtitlesCanvas != null)
            {
                _subtitlesCanvas.SetActive(enabled);
            }
        }
    }
}
