using UnityEngine;
using Story;
using System.Collections.Generic;

public class deerInteract : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Deer"
    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Deer is Dead" : "Dead Deer?";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
    }
}
