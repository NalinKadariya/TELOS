using UnityEngine;
using PlayerSystem;

public class ThrophyInteract : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private string _throphyDialogID = "Throphy";
    [SerializeField] private string _inventoryItemID = "Trophy";

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Already Inspected" : "Inspect Trophy";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;
        DialogManager.PlayDialog(_throphyDialogID);
        PlayerInventory.Instance.AddItem(_inventoryItemID);
        _hasInteracted = true;
    }
}
