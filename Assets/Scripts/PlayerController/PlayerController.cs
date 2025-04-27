using System;
using UnityEngine;
using CharacterControl.Manager;
using UnityEngine.TextCore;

namespace CharacterControl.PlayerControl
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(InputManager))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _hasAnimator;

        private int _xVelHash;
        private int _yVelHash;

        private float _xRotation;

        [Header("Movement Settings")]
        [SerializeField] private float _walkSpeed = 2f;
        [SerializeField] private float _runSpeed = 6f;
        [SerializeField] private float _animationBlendSpeed = 8.9f;

        [Header("Camera References")]
        [SerializeField] private Transform _Camera;      // Actual Camera
        [SerializeField] private Transform _CameraRoot;  // Where the camera should follow (animated head)

        [Header("Look Settings")]
        [SerializeField] private float _UpperLimit = -40f;
        [SerializeField] private float _LowerLimit = 70f;
        [SerializeField] private float _MouseSensitivity = 21.9f;

        private Vector2 _currentVelocity;

        // For smooth mouse movement
        private Vector2 _currentMouseDelta;
        private Vector2 _mouseDeltaVelocity;
        [SerializeField] private float _mouseSmoothTime = 0.03f; 


        private void Start()
        {
            // Cache components
            _hasAnimator = TryGetComponent(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            // Cache animation parameter hashes
            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");

            DisableCursor();
        }

        private void Update()
        {
            CamMovements();  
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;

            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, Time.deltaTime * _animationBlendSpeed);
            _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, Time.deltaTime * _animationBlendSpeed);

            Vector3 desiredVelocity = transform.TransformDirection(new Vector3(_currentVelocity.x, 0, _currentVelocity.y));

            Vector3 velocityChange = desiredVelocity - _playerRigidbody.linearVelocity;
            velocityChange.y = 0f; // Don't mess with gravity

            // If there's input, move normally
            if (_inputManager.Move != Vector2.zero)
            {
                _playerRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
            }
            else
            {
                _playerRigidbody.linearVelocity = Vector3.Lerp(_playerRigidbody.linearVelocity, Vector3.zero, Time.deltaTime * _animationBlendSpeed);
            }

            // Update animator
            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }


        private void CamMovements()
        {
            if (!_hasAnimator) return;

            Vector2 targetMouseDelta = _inputManager.Look;

            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _mouseDeltaVelocity, _mouseSmoothTime);

            transform.Rotate(Vector3.up * _currentMouseDelta.x * _MouseSensitivity * Time.deltaTime);

            _xRotation -= _currentMouseDelta.y * _MouseSensitivity * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, _UpperLimit, _LowerLimit);

            _Camera.position = _CameraRoot.position;
            _Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        }


        private void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void DisableCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void SetMouseSensitivity(float newSensitivity)
        {
            _MouseSensitivity = newSensitivity;
        }

    }
}