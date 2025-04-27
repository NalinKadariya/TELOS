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

        public bool IncreaseFlashlightIntensity { get; set; }
        public bool DecreaseFlashlightIntensity { get; set; }
        public bool OpenCloseStory { get; set; }
        public bool OpenCloseMap { get; set; }
        public bool OpenCloseInventory { get; set; }
        public bool PickupItem { get; private set; }


        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _increaseIntensityAction;
        private InputAction _decreaseIntensityAction;
        private InputAction _openCloseMapAction;
        private InputAction _openCloseStoryAction;
        private InputAction _pickupItemAction;


        private void Awake()
        {
            _currentMap = playerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction = _currentMap.FindAction("Run");
            _increaseIntensityAction = _currentMap.FindAction("IncreaseFlashlightIntensity");
            _decreaseIntensityAction = _currentMap.FindAction("DecreaseFlashlightIntensity");
            _openCloseMapAction = _currentMap.FindAction("OpenCloseMap");
            _openCloseStoryAction = _currentMap.FindAction("OpenCloseStory");
            _pickupItemAction = _currentMap.FindAction("PickupItem");

            // Add
            _moveAction.performed += OnMove;
            _lookAction.performed += OnLook;
            _runAction.performed += OnRun;
            _increaseIntensityAction.performed += OnIncreaseFlashlightIntensity;
            _decreaseIntensityAction.performed += OnDecreaseFlashlightIntensity;
            _openCloseMapAction.performed += OnOpenCloseMap;
            _openCloseStoryAction.performed += OnOpenCloseStory;
            _pickupItemAction.performed += OnPickupItem;

            // Release
            _moveAction.canceled += OnMove;
            _lookAction.canceled += OnLook;
            _runAction.canceled += OnRun;
            _increaseIntensityAction.canceled += OnIncreaseFlashlightIntensity;
            _decreaseIntensityAction.canceled += OnDecreaseFlashlightIntensity;
            _openCloseMapAction.canceled += OnOpenCloseMap;
            _openCloseStoryAction.canceled += OnOpenCloseStory;
            _pickupItemAction.canceled += OnPickupItem;
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

        private void OnOpenCloseMap(InputAction.CallbackContext context)
        {
            OpenCloseMap = context.ReadValueAsButton();
        }

        private void OnOpenCloseStory(InputAction.CallbackContext context)
        {
            OpenCloseStory = context.ReadValueAsButton();
        }

        private void OnPickupItem(InputAction.CallbackContext context)
        {
            PickupItem = context.ReadValueAsButton();
        }

    }
}