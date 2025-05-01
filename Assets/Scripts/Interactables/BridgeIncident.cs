using UnityEngine;
using Story;
using PlayerSystem;
using System.Collections.Generic;

public class BridgeIncident : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Bridge 1",
        "Bridge 2",
        "Bridge 3",
        "Bridge 4",
        "Bridge 5",
        "Bridge 6"
    };

    [Header("Inventory")]
    [SerializeField] private string _itemToGive = "BridgeIncident";

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? ": (" : "Recall the incident";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);

        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.AddItem(_itemToGive);
        
        _hasInteracted = true;
    }
}
