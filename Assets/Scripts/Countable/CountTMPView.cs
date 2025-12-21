namespace Countable
{
    using System;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    internal class CountTMPView : MonoBehaviour
    {
        [SerializeField] private string _format = "{0}";
        [SerializeField] private MonoBehaviour _countableComponent;
        
        private TMP_Text _textField;
        private ICountable _countable;
        
        private void Awake()
        {
            _textField = GetComponent<TMP_Text>();
            _countable = _countableComponent as ICountable ?? throw new InvalidOperationException();
        }

        private void OnEnable()
        {
            _countable.CountChanged += UpdateView;
            UpdateView();
        }
        
        private void OnDisable() =>
            _countable.CountChanged -= UpdateView;

        private void UpdateView() =>
            _textField.text = string.Format(_format, _countable is null ? string.Empty : _countable.Count);

        private void OnValidate()
        {
            if (_countableComponent is not (null or ICountable))
            {
                Debug.LogError($"{nameof(_countableComponent)} must inherit {nameof(ICountable)}");
                _countableComponent = null;
            }
        }
    }
}