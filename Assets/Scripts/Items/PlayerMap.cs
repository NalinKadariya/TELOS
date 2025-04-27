using UnityEngine;
using GameItemsNameSpace;
using UnityEngine.UI;
using System.Collections.Generic;
using PlayerSystem; // <-- Needed for PlayerInventory access

namespace GameItemsNameSpace.PlayerMapItem
{
    public class PlayerMap : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Canvas _mapCanvas;
        [SerializeField] private List<Canvas> _canvasesToDisable;
        [SerializeField] private List<Canvas> _canvasesToEnableWhenClosing;
        [SerializeField] private CharacterControl.Manager.InputManager _inputManager;
        [Header("Audio Clips")]
        [SerializeField] private AudioClip _openMapClip;
        [SerializeField] private AudioClip _closeMapClip;
        [SerializeField] private AudioSource _audioSource;

        [Header("Required Item")]
        [SerializeField] private string _requiredItemName = "Map";

        private void Start()
        {
            _mapCanvas.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_inputManager.OpenCloseMap)
            {
                if (PlayerInventory.Instance != null && PlayerInventory.Instance.HasItem(_requiredItemName))
                {
                    ToggleMap();
                }
                else
                {
                    Debug.Log("Player doesn't have a Map item!");
                }

                _inputManager.OpenCloseMap = false;
            }
        }

        private void ToggleMap()
        {
            if (_mapCanvas.gameObject.activeSelf)
            {
                GlobalUIManager.ForceCloseCurrent();
            }
            else
            {
                GlobalUIManager.RequestOpen(_mapCanvas, () =>
                {
                    foreach (Canvas c in _canvasesToEnableWhenClosing)
                        if (c != null) c.gameObject.SetActive(true);
                    PlayCloseAudio();
                });

                foreach (Canvas c in _canvasesToDisable)
                    if (c != null) c.gameObject.SetActive(false);

                PlayOpenAudio();
            }
        }

        private void PlayOpenAudio()
        {
            if (_audioSource == null) return;
            if (_openMapClip != null)
                _audioSource.PlayOneShot(_openMapClip);
        }

        private void PlayCloseAudio()
        {
            if (_audioSource == null) return;
            if (_closeMapClip != null)
                _audioSource.PlayOneShot(_closeMapClip);
        }
    }
}