using UnityEngine;
using PlayerSystem;

public class VaseInteract : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private string _initialDialogID = "Vase Quest";
    [SerializeField] private string _itemRequired = "Flowers";
    [SerializeField] private string _successDialogID = "Vase Found";
    [SerializeField] private string _rewardItemID = "VaseInteracted";

    [Header("References")]
    [SerializeField] private GameObject _currentVase;
    [SerializeField] private GameObject _replacedVase;

    private bool _hasPlayedInitialDialog = false;
    private bool _hasSwitched = false;

    public string GetInteractionText()
    {
        if (_hasSwitched)
            return "Vase Placed";

        if (!_hasPlayedInitialDialog)
            return "Inspect Vase";

        if (PlayerInventory.Instance.HasItem(_itemRequired))
            return "Place Vase";

        return "You need to find flowers for this vase. There are many near graveyard.";
    }

    public void Interact()
    {
        if (_hasSwitched)
            return;

        if (!_hasPlayedInitialDialog)
        {
            DialogManager.PlayDialog(_initialDialogID);
            _hasPlayedInitialDialog = true;
            return;
        }

        if (PlayerInventory.Instance.HasItem(_itemRequired))
        {
            if (_currentVase != null)
                _currentVase.SetActive(false);

            if (_replacedVase != null)
                _replacedVase.SetActive(true);

            DialogManager.PlayDialog(_successDialogID);
            PlayerInventory.Instance.AddItem(_rewardItemID);
            _hasSwitched = true;
        }
    }
}
