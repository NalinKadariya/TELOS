using UnityEngine;
using TMPro;
using PlayerSystem;
using System.Collections.Generic;
using CharacterControl.Manager;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _interactDistance = 3f;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private AudioClip _pickupSound;
    [SerializeField] private AudioSource _audioSource;

    [Header("UI")]
    [SerializeField] private Canvas _interactCanvas;
    [SerializeField] private TextMeshProUGUI _itemNameText;

    [Header("Lists")]
    [SerializeField] private List<GameObject> _pickupableItems = new List<GameObject>();

    [Header("Highlight Material")]
    [SerializeField] private Material _highlightMaterial;

    private GameObject _currentTarget;
    private Renderer _currentRenderer;
    private Material _originalMaterial;
    private bool _interactPressed = false;


    private void Start()
    {
        if (_interactCanvas != null)
            _interactCanvas.gameObject.SetActive(true);

        if (_itemNameText != null)
            _itemNameText.text = "";
    }

    private void Update()
    {
        DetectTarget();
        if (_inputManager.PickupItem && !_interactPressed)
        {
            HandleInteraction();
            _interactPressed = true;
        }
        else if (!_inputManager.PickupItem)
        {
            _interactPressed = false;
        }
    }

    private void DetectTarget()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (_pickupableItems.Contains(hitObject))
            {
                SetCurrentTarget(hitObject, $"[Pick Up: {hitObject.name}]");
                return;
            }
            else
            {
                IInteractable interactable = hitObject.GetComponentInParent<IInteractable>();
                if (interactable != null)
                {
                    SetCurrentTarget(hitObject, $"[{interactable.GetInteractionText()}]");
                    return;
                }
            }
        }

        ClearTarget();
    }

    private void SetCurrentTarget(GameObject target, string displayText)
    {
        if (_currentTarget != target)
        {
            RestoreOriginalMaterial();

            _currentTarget = target;
            _currentRenderer = _currentTarget.GetComponent<Renderer>();

            if (_currentRenderer != null)
            {
                _originalMaterial = _currentRenderer.material;
                _currentRenderer.material = _highlightMaterial;
            }
        }

        if (_itemNameText != null)
            _itemNameText.text = displayText;
    }

    private void HandleInteraction()
    {
        if (_currentTarget != null && _inputManager != null && _inputManager.PickupItem)
        {
            if (_pickupableItems.Contains(_currentTarget))
            {
                PlayerInventory.Instance.AddItem(_currentTarget.name);
                _pickupableItems.Remove(_currentTarget);
                if (_audioSource != null && _pickupSound != null)
                {
                    _audioSource.PlayOneShot(_pickupSound);
                }
                Destroy(_currentTarget);
                ClearTarget();
            }
            else
            {
                IInteractable interactable = _currentTarget.GetComponentInParent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
                ClearTarget();
            }
        }
    }

    private void ClearTarget()
    {
        RestoreOriginalMaterial();

        _currentTarget = null;
        _currentRenderer = null;
        _originalMaterial = null;

        if (_itemNameText != null)
            _itemNameText.text = "";
    }

    private void RestoreOriginalMaterial()
    {
        if (_currentRenderer != null && _originalMaterial != null)
            _currentRenderer.material = _originalMaterial;
    }
}
