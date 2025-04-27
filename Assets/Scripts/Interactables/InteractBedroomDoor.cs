using UnityEngine;
using PlayerSystem;

public class InteractBedroomDoor : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private float _openRotationY = 82.512f;
    [SerializeField] private float _closedRotationY = -3.185f;
    [SerializeField] private float _rotationSpeed = 100f;

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
        {
            AnimateDoor();
        }
    }

    public string GetInteractionText()
    {
        if (!HasRequiredItems())
            return "[Door Locked](Explore more)";

        return _isOpen ? "Close Door" : "Open Door";
    }

    public void Interact()
    {
        if (!HasRequiredItems())
            return;

        _isOpen = !_isOpen;
        _targetY = _isOpen ? _openRotationY : _closedRotationY;
        _isAnimating = true;
    }

    private bool HasRequiredItems()
    {
        return PlayerInventory.Instance.HasItem("HasTutorial") &&
               PlayerInventory.Instance.HasItem("HasObjectives");
               // PlayerInventory.Instance.HasItem("HasInteractedWithShelf"); 
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
