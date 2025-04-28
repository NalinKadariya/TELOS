using UnityEngine;
using PlayerSystem;
using System.Collections.Generic;
using SceneSwitchManagerNameSpace.SceneManagement;

public class InteractExitHouse : MonoBehaviour, IInteractable
{
    [Header("Required Items")]
    [SerializeField] private string _mapItemName = "Map";
    [SerializeField] private List<string> _otherRequiredItems = new List<string> { "HasInteractedWithFireplace" };

    public string GetInteractionText()
    {
        if (!HasMap())
            return "Map Missing";

        if (!HasOtherRequirements())
            return "Explore More";

        return "Exit House";
    }

    public void Interact()
    {
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
        var sceneSwitchManager = FindFirstObjectByType<SceneSwitchManager>();
        if (sceneSwitchManager != null)
        {
            sceneSwitchManager.SwitchHouseToForest();
        }
        else
        {
            Debug.LogError("SceneSwitchManager not found in scene!");
        }
    }
}
