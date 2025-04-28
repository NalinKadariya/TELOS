using UnityEngine;

public class SetParentToDontDestroy : MonoBehaviour
{
    private static GameObject _dontDestroyContainer;

    private void Awake()
    {
        if (_dontDestroyContainer == null)
        {
            _dontDestroyContainer = new GameObject("DontDestroyOnLoad_Container");
            DontDestroyOnLoad(_dontDestroyContainer);
        }

        transform.SetParent(_dontDestroyContainer.transform);
    }
}
