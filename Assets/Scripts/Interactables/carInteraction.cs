using UnityEngine;
using Story;
using System.Collections.Generic;
using PlayerSystem;

public class carInteraction : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Car 1",
        "Car 2",
        "Car 3",
        "Car 4",
        "Car 5"
    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Ended" : "Car Interact";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
        PlayerSystem.PlayerInventory.Instance.AddItem("CarInteracted");
    }
}
