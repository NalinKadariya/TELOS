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
        foreach (GameObject _birdObj in _birdSoundObjects)
        {
            StartCoroutine(_BirdNoiseRoutine(_birdObj));
        }
    }

    private IEnumerator _BirdNoiseRoutine(GameObject _birdObj)
    {
        AudioSource _audio = _birdObj.GetComponent<AudioSource>();
        if (_audio == null) yield break;

        while (true)
        {
            float _waitBeforePlay = Random.Range(_minWaitTime, _maxWaitTime);
            yield return new WaitForSeconds(_waitBeforePlay);

            _audio.Play();

            yield return new WaitForSeconds(_audio.clip.length);
        }
    }
}
