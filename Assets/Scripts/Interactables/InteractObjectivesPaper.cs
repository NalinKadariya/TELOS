using UnityEngine;
using Story;
using PlayerSystem;

public class InteractObjectivesPaper : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private StoryUIManager _storyUIManager;

    [Header("Story Info")]
    [SerializeField] private string _buttonName = "Objectives";
    [TextArea]
    [SerializeField] private string _storyText =
@"1: A
2: B
3: C";

    private bool _hasBeenCollected = false;

    public string GetInteractionText()
    {
        return _hasBeenCollected ? "Already Collected" : "Collect Objectives Paper";
    }

    public void Interact()
    {
        if (_hasBeenCollected)
            return;

        if (_storyUIManager != null)
            _storyUIManager.AddStory(_buttonName, _storyText);

        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.AddItem("HasObjectives");

        _hasBeenCollected = true;
        Destroy(gameObject);
    }
}
