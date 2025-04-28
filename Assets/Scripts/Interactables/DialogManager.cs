using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using Story;

public class DialogManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _subtitleText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Canvas _memoryCanvas;
    [SerializeField] private GameObject _memoryImagePrefab;
    [SerializeField] private Transform _memoryCanvasContent;
    [SerializeField] private float _memoryFlashDuration = 1.5f;
    [SerializeField] private StoryUIManager _storyUIManager;

    [System.Serializable]
    public class DialogEntry
    {
        public string ID;
        public AudioClip AudioClip;
        [TextArea]
        public string SubtitleText;
        public List<Sprite> MemoryImages = new List<Sprite>();
    }

    [Header("Dialogs")]
    [SerializeField] private List<DialogEntry> _dialogEntries = new List<DialogEntry>();

    private bool _isPlaying = false;
    private static DialogManager _instance;
    private Coroutine _memoryCoroutine;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        if (_memoryCanvas != null)
            _memoryCanvas.gameObject.SetActive(false);
    }

    public static void PlayDialog(string dialogID)
    {
        if (_instance == null || _instance._isPlaying)
            return;

        DialogEntry entry = _instance._dialogEntries.Find(x => x.ID == dialogID);
        if (entry != null)
            _instance.StartCoroutine(_instance.PlayDialogCoroutine(entry));
    }

    public static void PlayDialogsQueue(List<string> dialogIDs)
    {
        if (_instance == null || _instance._isPlaying)
            return;

        _instance.StartCoroutine(_instance.PlayDialogQueueCoroutine(dialogIDs));
    }

    private IEnumerator PlayDialogCoroutine(DialogEntry entry)
    {
        _isPlaying = true;

        if (entry.MemoryImages != null && entry.MemoryImages.Count > 0)
            _memoryCoroutine = StartCoroutine(PlaySingleMemoryFlash(entry.MemoryImages));

        if (_audioSource != null && entry.AudioClip != null)
            _audioSource.PlayOneShot(entry.AudioClip);
            _storyUIManager.AddStory(entry.ID, entry.SubtitleText);
            

        if (_subtitleText != null)
            _subtitleText.text = entry.SubtitleText;

        float duration = entry.AudioClip != null ? entry.AudioClip.length : 2f;
        yield return new WaitForSeconds(duration);

        if (_subtitleText != null)
            _subtitleText.text = "";

        if (_memoryCoroutine != null)
            StopMemoryFlashes();

        _isPlaying = false;
    }

    private IEnumerator PlayDialogQueueCoroutine(List<string> dialogIDs)
    {
        _isPlaying = true;

        foreach (var id in dialogIDs)
        {
            DialogEntry entry = _dialogEntries.Find(x => x.ID == id);

            if (entry != null)
            {
                if (entry.MemoryImages != null && entry.MemoryImages.Count > 0)
                    _memoryCoroutine = StartCoroutine(PlaySingleMemoryFlash(entry.MemoryImages));

                if (_audioSource != null && entry.AudioClip != null)
                    _audioSource.PlayOneShot(entry.AudioClip);

                if (_subtitleText != null)
                    _subtitleText.text = entry.SubtitleText;

                float duration = entry.AudioClip != null ? entry.AudioClip.length : 2f;
                yield return new WaitForSeconds(duration);

                if (_subtitleText != null)
                    _subtitleText.text = "";

                if (_memoryCoroutine != null)
                    StopMemoryFlashes();
            }
        }

        _isPlaying = false;
    }

    private IEnumerator PlaySingleMemoryFlash(List<Sprite> memorySprites)
    {
        _memoryCanvas.gameObject.SetActive(true);

        if (_memoryCanvasContent != null && _memoryImagePrefab != null && memorySprites.Count > 0)
        {
            Sprite randomSprite = memorySprites[Random.Range(0, memorySprites.Count)];

            GameObject flash = Instantiate(_memoryImagePrefab, _memoryCanvasContent);
            var img = flash.GetComponent<UnityEngine.UI.Image>();
            if (img != null)
                img.sprite = randomSprite;

            float randomZ = Random.Range(-10f, 10f);
            flash.transform.rotation = Quaternion.Euler(0f, 0f, randomZ);

            Destroy(flash, _memoryFlashDuration);
        }

        yield return null;
    }

    private void StopMemoryFlashes()
    {
        if (_memoryCoroutine != null)
            StopCoroutine(_memoryCoroutine);

        if (_memoryCanvas != null)
            _memoryCanvas.gameObject.SetActive(false);
    }
}
