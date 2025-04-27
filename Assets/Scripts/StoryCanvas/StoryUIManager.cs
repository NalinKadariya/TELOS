using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Story
{
    public class StoryUIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Canvas _storyCanvas;
        [SerializeField] private List<Canvas> _canvasesToDisable;
        [SerializeField] private List<Canvas> _canvasesToEnableWhenClosing;
        [SerializeField] private CharacterControl.Manager.InputManager _inputManager;
        [SerializeField] private Transform _leftPanelContent;
        [SerializeField] private Transform _rightPanelContent;
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private GameObject _paperImagePrefab;
        [SerializeField] private float _rotationRange = 20f;
        [SerializeField] private Vector2 _alphaRange = new Vector2(0.7f, 1f);
        [SerializeField] private Color _minPaperColor = new Color(1f, 1f, 0.8f);
        [SerializeField] private Color _maxPaperColor = new Color(1f, 1f, 0.5f);
    

        [Header("Audio Clips")]
        [SerializeField] private AudioClip _openStoryClip;
        [SerializeField] private AudioClip _closeStoryClip;
        [SerializeField] private AudioSource _audioSource;

        private GameObject _currentPaper;
        private TextMeshProUGUI _currentPaperText;
        private Image _currentPaperImage;
        private List<StoryEntry> _storyEntries = new List<StoryEntry>();

        private void Start()
        {
            _currentPaper = Instantiate(_paperImagePrefab, _rightPanelContent);
            _currentPaperText = _currentPaper.GetComponentInChildren<TextMeshProUGUI>();
            _currentPaperImage = _currentPaper.GetComponent<Image>();

            _storyCanvas.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_inputManager.OpenCloseStory)
            {
                ToggleStoryCanvas();
                _inputManager.OpenCloseStory = false;
            }
        }

       private void ToggleStoryCanvas()
        {
            if (_storyCanvas.gameObject.activeSelf)
            {
                GlobalUIManager.ForceCloseCurrent();
            }
            else
            {
                GlobalUIManager.RequestOpen(_storyCanvas, () =>
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
            if (_openStoryClip != null)
                _audioSource.PlayOneShot(_openStoryClip);
        }

        private void PlayCloseAudio()
        {
            if (_audioSource == null) return;
            if (_closeStoryClip != null)
                _audioSource.PlayOneShot(_closeStoryClip);
        }

        public void AddStory(string buttonName, string storyText)
        {
            GameObject newButton = Instantiate(_buttonPrefab, _leftPanelContent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;

            float randomRotation = Random.Range(-_rotationRange, _rotationRange);
            float randomAlpha = Random.Range(_alphaRange.x, _alphaRange.y);
            Color randomColor = Color.Lerp(_minPaperColor, _maxPaperColor, Random.value);

            StoryEntry newEntry = new StoryEntry
            {
                Button = newButton.GetComponent<Button>(),
                StoryText = storyText,
                RotationZ = randomRotation,
                Alpha = randomAlpha,
                PaperColor = randomColor
            };
            _storyEntries.Add(newEntry);

            newEntry.Button.onClick.AddListener(() =>
            {
                DisplayStory(newEntry);
            });

            if (_storyEntries.Count == 1)
            {
                DisplayStory(newEntry);
            }
        }

        private void DisplayStory(StoryEntry entry)
        {
            if (_currentPaperText != null)
            {
                _currentPaperText.text = entry.StoryText;
                _currentPaper.transform.rotation = Quaternion.Euler(0, 0, entry.RotationZ);

                if (_currentPaperImage != null)
                {
                    Color finalColor = entry.PaperColor;
                    finalColor.a = entry.Alpha;
                    _currentPaperImage.color = finalColor;
                }
            }
        }

        private class StoryEntry
        {
            public Button Button;
            public string StoryText;
            public float RotationZ;
            public float Alpha;
            public Color PaperColor;
        }
    }
}
