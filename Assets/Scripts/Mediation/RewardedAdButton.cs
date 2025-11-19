namespace Mediation
{
    using Bootstrap;
    using Common;
    using Reflex.Attributes;
    using UnityEngine;
    using UnityEngine.UI;

    internal class RewardedAdButton : AbstractButton
    {
        [SerializeField] private Button _buttonToComplete;
        
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        protected override void HandleClick() =>
            _services.Mediation.ShowRewardedAd(HandleComplete);

        private void HandleComplete() =>
            _buttonToComplete.onClick.Invoke();
    }
}