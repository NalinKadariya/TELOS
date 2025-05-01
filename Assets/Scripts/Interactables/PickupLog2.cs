using UnityEngine;
using PlayerSystem;
using Story;
using System.Collections.Generic;

public class PickupLog2 : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private string _itemID = "Log2";

    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Log 2 - 1",
        "Log 2 - 2",
        "Log 2 - 3",
        "Log 2 - 4",
        "Log 2 - 5",
        "Log 2 - 6"
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
