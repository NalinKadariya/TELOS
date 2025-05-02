using UnityEngine;
using PlayerSystem;
using Story;
using System.Collections.Generic;
using CharacterControl.PlayerControl;

public class GameEndTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private string _endGameItemID = "GameFinished";


    [Header("UI & Audio")]
    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private AudioSource _endingMusic;

    private bool _hasTriggered = false;
    private PlayerController _playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (_playerController == null)
            _playerController = other.GetComponent<PlayerController>();
        
        _playerController.enabled = false;

        _playerController.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _playerController.GetComponent<Animator>().enabled = false;
        if (_hasTriggered || !other.CompareTag(_playerTag))
            return;

        _hasTriggered = true;

        if (PlayerInventory.Instance.HasItem(_endGameItemID))
        {
            DialogManager.PlayDialogsQueue(new List<string> { "GameWon 1", "GameWon 2" });
            Debug.Log("Ending: Game Won");
        }
        else
        {
            DialogManager.PlayDialogsQueue(new List<string> { "Lost 1", "Lost 2" });
            Debug.Log("Ending: Game Over");
        }

        if (_fadeAnimator != null)
            _fadeAnimator.SetTrigger("FadeIn");

        
        if (_endingMusic != null)
        {
            _endingMusic.loop = true;
            _endingMusic.Play();
        }
    }
}
