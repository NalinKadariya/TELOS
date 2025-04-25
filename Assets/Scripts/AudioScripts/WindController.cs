using UnityEngine;

public class WindController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource _windSource;
    public float _lowWindVol = 0.1f;
    public float _highWindVol = 0.3f;
    public float _windChangeInterval = 10f;
    public float _transitionSpeed = 1f;

    private float _targetVol = 0f;
    private bool _isWindActive = false;
    private float _timer = 0f;

    void Start()
    {
        if (_windSource == null)
        {
            _windSource = GetComponent<AudioSource>();
            if (_windSource == null)
            {
                Debug.LogError("WindController requires an AudioSource!");
            }
        }

        _windSource.loop = true;
        _windSource.playOnAwake = false;
        _windSource.volume = 0f;

        EnableWind();
    }

    void Update()
    {
        if (!_isWindActive) return;

        _timer += Time.deltaTime;
        if (_timer >= _windChangeInterval)
        {
            _timer = 0f;
            float _chance = Random.value;
            _targetVol = _chance > 0.5f ? _highWindVol : _lowWindVol;
        }
        _windSource.volume = Mathf.Lerp(_windSource.volume, _targetVol, Time.deltaTime * _transitionSpeed);
    }

    public void EnableWind()
    {
        _isWindActive = true;
        _timer = _windChangeInterval; 
        _windSource.Play();
    }

    public void DisableWind()
    {
        _isWindActive = false;
        _windSource.Stop();
        _windSource.volume = 0f;
    }
}
