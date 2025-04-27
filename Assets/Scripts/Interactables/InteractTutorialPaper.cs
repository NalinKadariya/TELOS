using UnityEngine;
using Story;
using PlayerSystem; // <-- Needed for PlayerInventory

public class InteractTutorialPaper : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private StoryUIManager _storyUIManager;

    [Header("Story Info")]
    [SerializeField] private string _buttonName = "Tutorial Controls";
    [TextArea]
    [SerializeField] private string _storyText =
@"Q, E: light intensity
P: Pause/Settings
M: Map (map required)
Tabs: StoryLine";

    private bool _hasBeenCollected = false;

    public string GetInteractionText()
    {
        return _hasBeenCollected ? "Already Collected" : "Collect Tutorial Paper";
    }

    public void Interact()
    {
        if (_hasBeenCollected)
        {
            Debug.Log("Tutorial paper already collected.");
            return;
        }

        if (_storyUIManager != null)
        {
            _storyUIManager.AddStory(_buttonName, _storyText);
            Debug.Log("Tutorial paper added to Story UI!");
        }
        else
        {
            Debug.LogWarning("StoryUIManager reference missing on Tutorial Paper.");
        }

        // 🔥 Add "HasTutorial" to inventory
        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.AddItem("HasTutorial");
            Debug.Log("Added 'HasTutorial' to inventory!");
        }
        else
        {
            Debug.LogWarning("PlayerInventory instance missing!");
        }

        _hasBeenCollected = true;
    }
}
