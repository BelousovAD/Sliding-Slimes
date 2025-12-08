namespace Ability
{
    using TMPro;
    using UnityEngine;

    public class PriceView : MonoBehaviour
    {
        [SerializeField] private AbilityProvider _abilityProvider;
        [SerializeField] private GameObject _priceAdView;
        [SerializeField] private GameObject _priceView;
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
            _priceAdView.SetActive(_ability.Count == Ability.MinCount && _ability.IsRewardForAd);
            _priceView.SetActive(_ability.Count == Ability.MinCount && _ability.IsRewardForAd == false);
            _textField.text = _ability.Price.ToString();
        }
    }
}