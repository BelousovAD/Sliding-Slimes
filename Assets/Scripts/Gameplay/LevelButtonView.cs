namespace Gameplay
{
    using Reflex.Attributes;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(LevelButton))]
    internal class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private GameObject _lockIcon;
        [SerializeField] private GameObject _trophyIcon;
        [SerializeField] private TMP_Text _textField;
        [SerializeField] private string _format = "{0}";

        private LevelButton _levelButton;
        private Level _level;

        [Inject]
        private void Initialize(Level level) =>
            _level = level;

        private void Awake() =>
            _levelButton = GetComponent<LevelButton>();

        private void OnEnable()
        {
            _levelButton.NumberChanged += UpdateView;
            _level.AvailableChanged += UpdateView;
            UpdateView();
        }
        
        private void OnDisable()
        {
            _levelButton.NumberChanged -= UpdateView;
            _level.AvailableChanged -= UpdateView;
        }

        private void UpdateView()
        {
            _lockIcon.SetActive(_levelButton.Number > _level.Available);
            _trophyIcon.SetActive(_levelButton.Number < _level.Available);
            _textField.text = string.Format(_format, _levelButton.Number);
        }
    }
}