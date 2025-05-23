using UnityEngine;
using PlayerSystem;
using System.Collections;

public class InteractFireplace : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private GameObject _fireGameObject;
    [SerializeField] private string _missingLogsDialogID = "Fireplace";
    [SerializeField] private string _startFireDialogID = "Fireplace Fire";

    private bool _fireStarted = false;
    private bool _itemGiven = false;
    private bool _interactionTriggered = false;

    public string GetInteractionText()
    {
        if (_fireStarted)
            return "Warm and cozy.";

        if (HasAllLogs())
            return "Start a fire.";

        return "Collect logs to start a fire.";
    }

    public void Interact()
    {
        if (!_interactionTriggered)
        {
            _interactionTriggered = true;
            StartCoroutine(GiveItemAfterDelay(9f));
        }

        if (_fireStarted)
            return;

        if (!HasAllLogs())
        {
            DialogManager.PlayDialog(_missingLogsDialogID);
            return;
        }

        DialogManager.PlayDialog(_startFireDialogID);
        StartFire();
    }

    private bool HasAllLogs()
    {
        if (PlayerInventory.Instance == null)
            return false;

        return PlayerInventory.Instance.HasItem("Log1") &&
               PlayerInventory.Instance.HasItem("Log2") &&
               PlayerInventory.Instance.HasItem("Log3");
    }

    private void StartFire()
    {
        if (_fireGameObject != null)
            _fireGameObject.SetActive(true);

        _fireStarted = true;
    }

    private IEnumerator GiveItemAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!_itemGiven && PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.AddItem("HasInteractedWithFireplace");
            _itemGiven = true;
        }
    }
}
