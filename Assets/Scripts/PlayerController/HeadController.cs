using Unity.VisualScripting;
using UnityEngine;

namespace CharacterControl.PlayerControl 
{
    public class HeadController : MonoBehaviour
    {
        [SerializeField] private Transform _headBone;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _headRotationSpeed = 5f;
        [SerializeField] private float _maxHeadAngle = 60f;
        [SerializeField] private Vector3 _headOffset = Vector3.zero;

        private float _currentPitch = 0f;
        private float _accumulatedPitch = 0f;
        private float _lastCameraPitch = 0f;

        private void Start()
        {
            if (_headBone == null)
            {
                Debug.LogError("Head bone reference is missing!");
                enabled = false;
                return;
            }

            if (_camera == null)
            {
                _camera = Camera.main?.transform;
                if (_camera == null)
                {
                    Debug.LogError("No camera found!");
                    enabled = false;
                    return;
                }
            }

            _lastCameraPitch = GetCameraPitch();
        }

        private void LateUpdate()
        {
            if (_headBone == null || _camera == null) return;

            float currentCameraPitch = GetCameraPitch();
            float pitchDelta = Mathf.DeltaAngle(_lastCameraPitch, currentCameraPitch);

            _accumulatedPitch += pitchDelta;
            _accumulatedPitch = Mathf.Clamp(_accumulatedPitch, -_maxHeadAngle, _maxHeadAngle);

            _lastCameraPitch = currentCameraPitch;

            _currentPitch = Mathf.Lerp(_currentPitch, _accumulatedPitch, Time.deltaTime * _headRotationSpeed);

            Quaternion targetRotation = Quaternion.Euler(_currentPitch, 0f, 0f);
            _headBone.localRotation = targetRotation;

            if (_headOffset != Vector3.zero)
            {
                _headBone.localPosition += _headOffset;
            }
        }

        private float GetCameraPitch()
        {
            float pitch = _camera.eulerAngles.x;
            return (pitch > 180f) ? pitch - 360f : pitch;
        }
    }
}