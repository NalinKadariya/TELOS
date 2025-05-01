using UnityEngine;
using PlayerSystem;
using Story;
using System.Collections.Generic;

public class PickupLog1 : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private string _itemID = "Log1";

    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Log 1 - 1",
        "Log 1 - 2",
        "Log 1 - 3"
    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Already Collected" : "Pick up Log";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        PlayerInventory.Instance.AddItem(_itemID);
        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;

        Destroy(gameObject);
    }
}