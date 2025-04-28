using UnityEngine;
using PlayerSystem;
using System.Collections.Generic;

public class InteractExitHouse : MonoBehaviour, IInteractable
{
    [Header("Required Items")]
    [SerializeField] private string _mapItemName = "Map";
    [SerializeField] private List<string> _otherRequiredItems = new List<string> { "HasInteractedWithFireplace" };

    private bool _hasExited = false;

    public string GetInteractionText()
    {
        if (_hasExited)
            return "Already Exited";

        if (!HasMap())
            return "Map Missing";

        if (!HasOtherRequirements())
            return "Explore More";

        return "Exit House";
    }

    public void Interact()
    {
        if (_hasExited)
            return;

        if (!HasMap() || !HasOtherRequirements())
            return; 

        ExitHouse();
    }

    private bool HasMap()
    {
        if (PlayerInventory.Instance == null)
            return false;

        return PlayerInventory.Instance.HasItem(_mapItemName);
    }

    private bool HasOtherRequirements()
    {
        if (PlayerInventory.Instance == null)
            return false;

        foreach (var item in _otherRequiredItems)
        {
            if (!PlayerInventory.Instance.HasItem(item))
                return false;
        }
        return true;
    }

    private void ExitHouse()
    {
        _hasExited = true;
    }
}
