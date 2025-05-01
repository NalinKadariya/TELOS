using UnityEngine;
using System.Collections.Generic;
using PlayerSystem;


public class InteractInventoryDoorDialog : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private List<string> _requiredItems = new List<string>();
    [SerializeField] private string _lockedFirstDialogID = "Cupboard 1";
    [SerializeField] private string _lockedRepeatDialogID = "Cupboard 2";
    [SerializeField] private string _unlockDialogID = "CupboardOpened";
    [SerializeField] private float _openRotationY = -59f;
    [SerializeField] private float _closedRotationY = -180f;
    [SerializeField] private float _rotationSpeed = 100f;

    private bool _hasPlayedFirstLockedDialog = false;
    private bool _isOpen = false;
    private bool _isAnimating = false;
    private float _targetY;

    private void Start()
    {
        _targetY = transform.localEulerAngles.y;
    }

    private void Update()
    {
        if (_isAnimating)
            AnimateDoor();
    }

    public string GetInteractionText()
    {
        if (!HasRequiredItems())
            return "Locked";

        return _isOpen ? "Close Door" : "Open Door";
    }

    public void Interact()
    {
        if (!HasRequiredItems())
        {
            if (!_hasPlayedFirstLockedDialog)
            {
                DialogManager.PlayDialog(_lockedFirstDialogID);
                PlayerInventory.Instance.AddItem("HasInteractedWithShelf");
                
                _hasPlayedFirstLockedDialog = true;
            }
            else
            {
                DialogManager.PlayDialog(_lockedRepeatDialogID);
            }
            return;
        }

        if (!_isOpen)
            DialogManager.PlayDialog(_unlockDialogID);

        _isOpen = !_isOpen;
        _targetY = _isOpen ? _openRotationY : _closedRotationY;
        _isAnimating = true;

        if (HasRequiredItems())
        {
            foreach (var item in _requiredItems)
            {
                PlayerInventory.Instance.AddItem("GameFinished");
            }
        }
    }

    private bool HasRequiredItems()
    {
        if (PlayerInventory.Instance == null)
            return false;

        foreach (var item in _requiredItems)
        {
            if (!PlayerInventory.Instance.HasItem(item))
                return false;
        }
        return true;
    }

    private void AnimateDoor()
    {
        Vector3 currentRotation = transform.localEulerAngles;
        float newY = Mathf.MoveTowardsAngle(currentRotation.y, _targetY, _rotationSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(currentRotation.x, newY, currentRotation.z);

        if (Mathf.Abs(Mathf.DeltaAngle(newY, _targetY)) < 0.5f)
        {
            transform.localEulerAngles = new Vector3(currentRotation.x, _targetY, currentRotation.z);
            _isAnimating = false;
        }
    }
}
