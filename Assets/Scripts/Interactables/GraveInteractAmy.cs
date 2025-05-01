using UnityEngine;
using Story;
using System.Collections.Generic;
using PlayerSystem;

public class GraveInteractAmy : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Amy Grave 1",
        "Amy Grave 2",
        "Amy Grave 3",
        "Amy Grave 4",
        "Amy Grave 5"

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
        PlayerSystem.PlayerInventory.Instance.AddItem("AmysGraveSeen");
    }
}
