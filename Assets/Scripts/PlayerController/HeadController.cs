using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterControl.Manager;

namespace CharacterControl.PlayerControl 
{
    public class HeadController : MonoBehaviour
    {
        [SerializeField] private Transform _headBone;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _headRotationSpeed = 30f;
        [SerializeField] private float _maxHeadAngle = 220f;
        [SerializeField] private Vector3 _headOffset = Vector3.zero;
        
        private Quaternion _lastCameraRotation;
        private Quaternion _targetRotation;
        private bool _cameraRotationChanged = false;

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
                _camera = Camera.main.transform;
            }
            
            if (_camera != null)
            {
                _lastCameraRotation = _camera.rotation;
                _targetRotation = _headBone.rotation;
            }
        }

        private void LateUpdate()
        {
            if (_headBone == null || _camera == null) return;

            if (_camera.rotation != _lastCameraRotation)
            {
                _cameraRotationChanged = true;
                UpdateHeadRotation();
                _lastCameraRotation = _camera.rotation;
            }
            else if (_cameraRotationChanged)
            {
                _headBone.rotation = Quaternion.Slerp(
                    _headBone.rotation, 
                    _targetRotation, 
                    Time.deltaTime * _headRotationSpeed
                );
                
                if (Quaternion.Angle(_headBone.rotation, _targetRotation) < 0.1f)
                {
                    _cameraRotationChanged = false;
                }
            }
            
            if (_headOffset != Vector3.zero)
            {
                _headBone.localPosition = _headBone.localPosition.normalized + _headOffset;
            }
        }

        private void UpdateHeadRotation()
        {
            Vector3 cameraForward = _camera.forward;
            
            Vector3 horizontalForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            
            Quaternion targetYawRotation = Quaternion.LookRotation(horizontalForward, Vector3.up);
            
            float verticalAngle = Mathf.Clamp(
                Vector3.SignedAngle(horizontalForward, cameraForward, transform.right),
                -_maxHeadAngle, 
                _maxHeadAngle
            );
            
            _targetRotation = targetYawRotation * Quaternion.Euler(verticalAngle, 0, 0);
            
            _headBone.rotation = Quaternion.Slerp(
                _headBone.rotation, 
                _targetRotation, 
                Time.deltaTime * _headRotationSpeed
            );
        }
    }
}