using UnityEngine;

public class PianoMemories : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private string _pianoMemoryDialogID = "Piano Memories";

    public string GetInteractionText()
    {
        return "Piano Memories";
    }

    public void Interact()
    {
        DialogManager.PlayDialog(_pianoMemoryDialogID);
    }
}
