using UnityEngine;
using Story;
using System.Collections.Generic;
using PlayerSystem;

public class PlacingFlowerInGrave : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Placing 1",
        "Placing 2",
        "Placing 3",
        "Placing 4",
        "Placing 5",
        "Placing 6",
        "Placing 7",
        "Placing 8",
        "Placing 9",
        "Placing 10",
        "Placing 11"

    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Seen" : "Amy's Grave";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
        PlayerSystem.PlayerInventory.Instance.AddItem("AmysGravenGivenFlower");
    }
}
