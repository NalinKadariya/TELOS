using UnityEngine;
using SceneSwitchManagerNameSpace.SceneManagement;

public class InteractEnterHouse : MonoBehaviour, IInteractable
{
    public string GetInteractionText()
    {
        return "Enter House";
    }

    public void Interact()
    {
        EnterHouse();
    }

    private void EnterHouse()
    {
        var sceneSwitchManager = FindFirstObjectByType<SceneSwitchManager>();
        if (sceneSwitchManager != null)
        {
            sceneSwitchManager.SwitchForestToHouse();
        }
        else
        {
            Debug.LogError("SceneSwitchManager not found in scene!");
        }
    }
}
