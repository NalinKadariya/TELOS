using UnityEngine;
using CharacterControl.Manager;

namespace GameItemsNameSpace.FlashLightItem
{
    public class CameraFlashlightController : MonoBehaviour
    {
        [Header("Flashlight Settings")]
        [SerializeField] private Light _flashlight;
        [SerializeField] private float[] _intensitySteps = { 0f, 2f, 5f, 10f };
        [SerializeField] private int _startingStepIndex = 2; // Start at index 2

        [Header("Audio Settings")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _intensityUpClip;
        [SerializeField] private AudioClip _intensityDownClip;

        [Header("References")]
        [SerializeField] private InputManager _inputManager;

        private int _currentStepIndex;

        private void Start()
        {
            _currentStepIndex = Mathf.Clamp(_startingStepIndex, 0, _intensitySteps.Length - 1);

            if (_flashlight != null)
            {
                _flashlight.intensity = _intensitySteps[_currentStepIndex];
                _flashlight.enabled = true;
            }
        }

        private void Update()
        {
            if (_inputManager == null) return;

            HandleIntensityControl();
        }

        private void HandleIntensityControl()
        {
            if (_inputManager.IncreaseFlashlightIntensity)
            {
                _inputManager.IncreaseFlashlightIntensity = false;

                if (_currentStepIndex < _intensitySteps.Length - 1)
                {
                    _currentStepIndex++;
                    UpdateFlashlightIntensity();
                    PlayOneShot(_intensityUpClip);
                }
            }

            if (_inputManager.DecreaseFlashlightIntensity)
            {
                _inputManager.DecreaseFlashlightIntensity = false;

                if (_currentStepIndex > 0)
                {
                    _currentStepIndex--;
                    UpdateFlashlightIntensity();
                    PlayOneShot(_intensityDownClip);
                }
            }
        }

        private void UpdateFlashlightIntensity()
        {
            if (_flashlight != null)
            {
                _flashlight.intensity = _intensitySteps[_currentStepIndex];
            }
        }

        private void PlayOneShot(AudioClip clip)
        {
            if (_audioSource != null && clip != null)
            {
                _audioSource.PlayOneShot(clip);
            }
        }
    }
}
