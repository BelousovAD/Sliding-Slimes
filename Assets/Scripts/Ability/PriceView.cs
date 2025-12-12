namespace Ability
{
    using Currency;
    using Reflex.Attributes;
    using TMPro;
    using UnityEngine;

    public class PriceView : MonoBehaviour
    {
        [SerializeField] private AbilityProvider _abilityProvider;
        [SerializeField] private GameObject _priceAdView;
        [SerializeField] private GameObject _priceView;
        [SerializeField] private TMP_Text _textField;

        private Ability _ability;
        private Money _money;

        [Inject]
        private void Initialize(Money money) =>
            _money = money;

        private void Start()
        {
            _ability = _abilityProvider.Ability;
            _ability.CountChanged += UpdateView;
            _money.Changed += UpdateView;
            UpdateView();
        }

        private void OnDestroy()
        {
            _ability.CountChanged -= UpdateView;
            _money.Changed -= UpdateView;
        }

        private void UpdateView()
        {
            _priceAdView.SetActive(_ability.Count == Ability.MinCount &&
                                   (_ability.IsOnlyRewardForAd || _money.Value < _ability.Price));
            _priceView.SetActive(_ability.Count == Ability.MinCount && _ability.IsOnlyRewardForAd == false &&
                                 _money.Value >= _ability.Price);
            _textField.text = _ability.Price.ToString();
        }
    }
}