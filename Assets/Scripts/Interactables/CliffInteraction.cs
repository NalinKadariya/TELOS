using UnityEngine;
using Story;
using System.Collections.Generic;
using PlayerSystem;

public class CliffInteraction : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Cliff 1",
        "Cliff 2",
        "Cliff 3",
        "Cliff 4",
        "Cliff 5"
    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Ended" : "Hospital";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
        PlayerSystem.PlayerInventory.Instance.AddItem("CliffCliffert");
    }
}
