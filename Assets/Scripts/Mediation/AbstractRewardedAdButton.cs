namespace Mediation
{
    using Bootstrap;
    using Common;
    using Reflex.Attributes;

    internal abstract class AbstractRewardedAdButton : AbstractButton
    {
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        protected override void HandleClick() =>
            _services.Mediation.ShowRewardedAd(HandleComplete);

        protected abstract void HandleComplete();
    }
}