using UnityEngine;
using Story;
using System.Collections.Generic;
using PlayerSystem;

public class KnifeInteract : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Knife Dialog 1",
        "Knife Dialog 2",
        "Knife Dialog 3",
        "Knife Dialog 4"
    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "I commited a murder..." : "My Murder";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
        PlayerSystem.PlayerInventory.Instance.AddItem("KnifeInteracted");
    }
}
