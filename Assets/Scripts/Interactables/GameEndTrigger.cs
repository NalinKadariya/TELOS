using UnityEngine;
using UnityEngine.Playables;
using PlayerSystem;
using Story;
using System.Collections.Generic;

public class GameEndTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private string _endGameItemID = "GameFinished";

    [Header("Timelines")]
    [SerializeField] private PlayableDirector _gameFinishedTimeline;
    [SerializeField] private PlayableDirector _gameOverTimeline;

    private bool _hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggered || other.CompareTag(_playerTag) == false)
            return;

        _hasTriggered = true;

        if (PlayerInventory.Instance.HasItem(_endGameItemID))
        {
            if (_gameFinishedTimeline != null)
                _gameFinishedTimeline.Play();

            DialogManager.PlayDialogsQueue(new List<string> { "GameWon 1", "GameWon 2" });
            Debug.Log("Ending 1.");
        }
        else
        {
            if (_gameOverTimeline != null)
                _gameOverTimeline.Play();

            DialogManager.PlayDialogsQueue(new List<string> { "Lost 1", "Lost 2" });
            Debug.Log("Ending 2.");
        }
    }
}
