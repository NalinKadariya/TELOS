using UnityEngine;
using PlayerSystem;

public class PickupLog3 : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private string _itemID = "Log3";

    public string GetInteractionText()
    {
        return "Pick up Log";
    }

    public void Interact()
    {
        PlayerInventory.Instance.AddItem(_itemID);
        Destroy(gameObject);
    }
}
