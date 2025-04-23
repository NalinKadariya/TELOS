using System;
using System.Collections;
using System.Collections.Generic;
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
        
        [SerializeField] private float _walkSpeed = 2f;
        [SerializeField] private float _runSpeed = 6f;
        [SerializeField] private float _animationBlendSpeed = 8.9f;
        [SerializeField] private Transform _Camera;
        [SerializeField] private Transform _CameraRoot;
        [SerializeField] private float _UpperLimit = -40f;
        [SerializeField] private float _LowerLimit = 70f;
        [SerializeField] private float _MouseSensitivity = 21.9f;

        private Vector2 _currentVelocity;

        private void Start()
        {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

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
            if (_inputManager.Move == Vector2.zero) targetSpeed = 0.1f;

            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, Time.fixedDeltaTime*_animationBlendSpeed);
            _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, Time.fixedDeltaTime*_animationBlendSpeed);

            var xVelDifference = _currentVelocity.x - _playerRigidbody.linearVelocity.x;
            var zVelDifference = _currentVelocity.y - _playerRigidbody.linearVelocity.z;


            // Add force
            _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);
            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }

        private void CamMovements() {
            if (!_hasAnimator) return;

            var Mouse_X = _inputManager.Look.x;
            var Mouse_Y = _inputManager.Look.y;
            
            _Camera.position = _CameraRoot.position;

            _xRotation -= Mouse_Y * _MouseSensitivity * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, _UpperLimit, _LowerLimit);

            _Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            transform.Rotate(Vector3.up * Mouse_X * _MouseSensitivity * Time.deltaTime);
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
        
    }
}