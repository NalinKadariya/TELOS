using UnityEngine;
using GameItemsNameSpace;

namespace GameItemsNameSpace.PlayerMapItem
{
    public class PlayerMap : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject mapUI;
        [SerializeField] private CharacterControl.Manager.InputManager inputManager;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip openMapClip;
        [SerializeField] private AudioClip closeMapClip;
        [SerializeField] private AudioSource audioSource;

        private bool isMapOpen = false;

        private void Update()
        {
            if (inputManager.OpenCloseMap)
            {
                ToggleMap();
                inputManager.OpenCloseMap = false; 
            }
        }

        private void ToggleMap()
        {
            isMapOpen = !isMapOpen;
            mapUI.SetActive(isMapOpen);

            PlayMapAudio();
        }

        private void PlayMapAudio()
        {
            if (audioSource == null) return;

            AudioClip clipToPlay = isMapOpen ? openMapClip : closeMapClip;
            if (clipToPlay != null)
            {
                audioSource.PlayOneShot(clipToPlay);
            }
        }
    }
}
