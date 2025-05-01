using UnityEngine;
using Story;
using System.Collections.Generic;
using PlayerSystem;

public class DeadBody : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Dead Body 1",
        "Dead Body 2",
        "Dead Body 3"
    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Is that... me?" : "Dead Body?";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
        PlayerSystem.PlayerInventory.Instance.AddItem("DeadBodyInteracted");
    }
}
