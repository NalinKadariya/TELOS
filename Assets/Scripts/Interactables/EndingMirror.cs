using UnityEngine;
using Story;
using PlayerSystem;

public class EndingMirror : MonoBehaviour, IInteractable
{
    [Header("Dialog Sequence")]
    [SerializeField] private string _endingDialog = "Mirror End";

    private bool _hasInteracted = false;

    public string GetInteractionText()
    {
        return _hasInteracted ? "Ended" : "Look into Mirror";
    }

    public void Interact()
    {
        if (_hasInteracted)
            return;

        DialogManager.PlayDialog(_endingDialog);
        _hasInteracted = true;
        PlayerInventory.Instance.AddItem("GameFinished");
    }
}
