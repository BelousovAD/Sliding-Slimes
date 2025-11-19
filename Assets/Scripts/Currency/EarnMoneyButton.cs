namespace Currency
{
    using Bootstrap;
    using Common;
    using Reflex.Attributes;
    using UnityEngine;

    internal class EarnMoneyButton : AbstractButton
    {
        [SerializeField, Min(0)] private int _amount;
        [SerializeField] private bool _isRewardedAd;

        private Money _money;
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(Money money, SavvyServicesProvider servicesProvider)
        {
            _money = money;
            _services = servicesProvider;
        }

        protected override void HandleClick()
        {
            if (_isRewardedAd)
            {
                _services.Mediation.ShowRewardedAd(Earn);
            }
            else
            {
                Earn();
            }
        }

        private void Earn() =>
            _money.Earn(_amount);
    }
}