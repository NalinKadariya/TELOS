using UnityEngine;

public class SetParentToDontDestroy : MonoBehaviour
{
    private static SetParentToDontDestroy  _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
