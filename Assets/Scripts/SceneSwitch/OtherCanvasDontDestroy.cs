using UnityEngine;

public class OtherCanvasDontDestroy : MonoBehaviour
{
    private static OtherCanvasDontDestroy _instance;

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
