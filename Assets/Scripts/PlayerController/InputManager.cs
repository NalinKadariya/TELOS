using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

namespace CharacterControl.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        public Vector2 Move {get; private set;}
        public Vector2 Look {get; private set;}
        public bool Run {get; private set;}
        // Flashlight
        public bool IncreaseFlashlightIntensity { get; set; }
        public bool DecreaseFlashlightIntensity { get; set; }


        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _increaseIntensityAction;
        private InputAction _decreaseIntensityAction;

        private void Awake()
        {
            _currentMap = playerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction = _currentMap.FindAction("Run");

            // Flashlight actions
            _increaseIntensityAction = _currentMap.FindAction("IncreaseFlashlightIntensity");
            _decreaseIntensityAction = _currentMap.FindAction("DecreaseFlashlightIntensity");

            // Add
            _moveAction.performed += OnMove;
            _lookAction.performed += OnLook;
            _runAction.performed += OnRun;
            _increaseIntensityAction.performed += OnIncreaseFlashlightIntensity;
            _decreaseIntensityAction.performed += OnDecreaseFlashlightIntensity;

            // Release
            _moveAction.canceled += OnMove;
            _lookAction.canceled += OnLook;
            _runAction.canceled += OnRun;
            _increaseIntensityAction.canceled += OnIncreaseFlashlightIntensity;
            _decreaseIntensityAction.canceled += OnDecreaseFlashlightIntensity;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void OnEnable()
        {
            _currentMap.Enable();
        }

        private void OnDisable()
        {
            _currentMap.Disable();
        }

        private void OnIncreaseFlashlightIntensity(InputAction.CallbackContext context)
        {
           IncreaseFlashlightIntensity = context.ReadValueAsButton();
        }

        private void OnDecreaseFlashlightIntensity(InputAction.CallbackContext context)
        {
            DecreaseFlashlightIntensity = context.ReadValueAsButton();
        }

    }
}