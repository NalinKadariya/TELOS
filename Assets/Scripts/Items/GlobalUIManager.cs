using UnityEngine;
using System.Collections.Generic;

public class GlobalUIManager : MonoBehaviour
{
    private static GlobalUIManager _instance;
    private Canvas _currentOpenCanvas;
    private System.Action _onCloseCurrent;

    [Header("Cursor Unlock Settings")]
    [SerializeField] private List<Canvas> _canvasesThatUnlockCursor = new List<Canvas>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public static void RequestOpen(Canvas canvasToOpen, System.Action onCloseCallback)
    {
        if (_instance == null) return;

        if (_instance._currentOpenCanvas != null)
        {
            _instance._currentOpenCanvas.gameObject.SetActive(false);
            _instance._onCloseCurrent?.Invoke();
        }

        _instance._currentOpenCanvas = canvasToOpen;
        _instance._onCloseCurrent = onCloseCallback;

        if (canvasToOpen != null)
            canvasToOpen.gameObject.SetActive(true);

        _instance.UpdateCursorState();
    }

    public static void ForceCloseCurrent()
    {
        if (_instance == null)
            return;

        if (_instance._currentOpenCanvas != null)
        {
            _instance._currentOpenCanvas.gameObject.SetActive(false);
            _instance._onCloseCurrent?.Invoke();
            _instance._currentOpenCanvas = null;
            _instance._onCloseCurrent = null;
        }

        _instance.UpdateCursorState();
    }

    private void UpdateCursorState()
    {
        bool shouldUnlock = false;

        if (_currentOpenCanvas != null && _canvasesThatUnlockCursor.Contains(_currentOpenCanvas))
        {
            shouldUnlock = true;
        }

        if (shouldUnlock)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
