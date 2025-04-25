using System.Collections;
using UnityEngine;

public class SpecialBirdNoises : MonoBehaviour
{
    [SerializeField] private GameObject[] _birdSoundObjects;
    
    [Header("Bird Sound Settings")]
    [SerializeField] private float _minWaitTime = 5f;
    [SerializeField] private float _maxWaitTime = 10f;

    private void Start()
    {
        foreach (GameObject _bird in _birdSoundObjects)
        {
            StartCoroutine(_BirdNoiseRoutine(_bird));
        }
    }

    private IEnumerator _BirdNoiseRoutine(GameObject _bird)
    {
        // Get all child AudioSources
        AudioSource[] _audioSources = _bird.GetComponentsInChildren<AudioSource>();
        if (_audioSources == null || _audioSources.Length == 0) yield break;

        while (true)
        {
            float _waitBeforePlay = Random.Range(_minWaitTime, _maxWaitTime);
            yield return new WaitForSeconds(_waitBeforePlay);

            AudioSource _selectedSource = _audioSources[Random.Range(0, _audioSources.Length)];
            if (_selectedSource.clip != null)
            {
                _selectedSource.Play();
                yield return new WaitForSeconds(_selectedSource.clip.length);
            }
        }
    }
}
