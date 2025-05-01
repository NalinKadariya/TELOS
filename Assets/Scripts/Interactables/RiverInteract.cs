using UnityEngine;
using Story;
using System.Collections.Generic;

public class RiverInteract : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "River 1",
        "River 2",
        "River 3",
        "River 4"
    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "You reflect by the river." : "Approach the river";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
    }
}
