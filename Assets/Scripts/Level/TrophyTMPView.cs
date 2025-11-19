namespace Level
{
    using Reflex.Attributes;
    using Savvy.Extensions;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    internal class TrophyTMPView : MonoBehaviour
    {
        [SerializeField] private string _format = "{0}";
        
        private TMP_Text _textField;
        private Level _level;

        [Inject]
        private void Initialize(Level level) =>
            _level = level;

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            _level.AvailableChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _level.AvailableChanged -= UpdateView;

        private void UpdateView() =>
            _textField.text = string.Format(_format, (_level.Available - 1).ToNumsFormat());
    }
}