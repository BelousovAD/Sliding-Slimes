using TMPro;
using UnityEngine;

namespace Ability
{
    public class CountView : MonoBehaviour
    {
        [SerializeField] private AbilityProvider _abilityProvider;
        [SerializeField] private GameObject _countView;
        [SerializeField] private TMP_Text _textField;

        private Ability _ability;

        private void Start()
        {
            _ability = _abilityProvider.Ability;
            _ability.CountChanged += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _ability.CountChanged -= UpdateView;

        private void UpdateView()
        {
            _countView.SetActive(_ability.Count > Ability.MinCount);
            _textField.text = _ability.Count.ToString();
        }
    }
}