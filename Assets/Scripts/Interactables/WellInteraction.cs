using UnityEngine;
using Story;
using System.Collections.Generic;
using PlayerSystem;

public class WellInteraction : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Well 1",
        "Well 2",
        "Well 3",
        "Well 4",
        "Well 5"
    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Ended" : "Well Interact";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
        PlayerSystem.PlayerInventory.Instance.AddItem("WellInteracted");
    }
}
