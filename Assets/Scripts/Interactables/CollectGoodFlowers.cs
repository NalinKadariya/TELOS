using UnityEngine;
using Story;
using System.Collections.Generic;
using PlayerSystem;

public class CollectGoodFlowers : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private List<string> _dialogSequence = new List<string>
    {
        "Flower 1",
        "Flower 2",
        "Flower 3",
        "Flower 4",
        "Flower 5",
        "Flower 6",
        "Flower 7"

    };

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Collected" : "Collect Flowers";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialogsQueue(_dialogSequence);
        _hasInteracted = true;
        PlayerSystem.PlayerInventory.Instance.AddItem("Flowers");
        Destroy(gameObject);
    }
}
