using UnityEngine;
using CharacterControl.Manager;

public class MenuCameraLook : MonoBehaviour
{
    [SerializeField] private InputManager inputManager; 
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private float maxRotation = 5f;  
    [SerializeField] private float smoothSpeed = 5f;  
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float musicFadeInTime = 2f;

    private Quaternion initialRotation;
    private Vector2 currentLook; 

    private void Start()
    {
        initialRotation = transform.rotation;
        if (musicSource != null && musicSource.clip != null)
        {
            musicSource.volume = 0f; 
            musicSource.Play(); 
            StartCoroutine(FadeInMusic(musicFadeInTime)); 
        }
    }

    private void Update()
    {
        if (inputManager == null)
            return;

        Vector2 lookInput = inputManager.Look;

        currentLook = Vector2.Lerp(currentLook, lookInput, Time.deltaTime * smoothSpeed);

        float yaw = Mathf.Clamp(currentLook.x * sensitivity, -maxRotation, maxRotation);
        float pitch = Mathf.Clamp(currentLook.y * sensitivity, -maxRotation, maxRotation);

        Quaternion targetRotation = initialRotation * Quaternion.Euler(-pitch, yaw, 0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }

    private System.Collections.IEnumerator FadeInMusic(float duration)
    {
        float startVolume = 0f;
        float targetVolume = .7f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            yield return null;
        }

        musicSource.volume = targetVolume;
    } 
}
