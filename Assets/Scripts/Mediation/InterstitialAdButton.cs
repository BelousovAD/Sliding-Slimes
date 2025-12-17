namespace Mediation
{
    using Bootstrap;
    using Common;
    using Reflex.Attributes;

    public class InterstitialAdButton : AbstractButton
    {
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        protected override void HandleClick() =>
            _services.Mediation.ShowInterstitialAd();
    }
}