namespace Map
{
    using System;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    public class CountTMPView : MonoBehaviour
    {
        [SerializeField] private string _format = "{0}";
        [SerializeField] private MonoBehaviour _countableProvider;
        
        private TMP_Text _textField;
        private ICountable _countable;

        private void Awake()
        {
            _textField = GetComponent<TMP_Text>();
            _countable = ((IModelProvider)_countableProvider).Model as ICountable
                         ?? throw new InvalidOperationException();
        }

        private void OnEnable()
        {
            _countable.CountChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _countable.CountChanged -= UpdateView;

        private void UpdateView() =>
            _textField.text = string.Format(_format, _countable.Count);

        private void OnValidate()
        {
            if (_countableProvider is null or IModelProvider)
            {
                return;
            }

            Debug.LogError($"{nameof(_countableProvider)} must inherit {nameof(IModelProvider)}");
            _countableProvider = null;
        }
    }
}