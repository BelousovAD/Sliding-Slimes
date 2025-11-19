namespace Mediation
{
    using Bootstrap;
    using Reflex.Attributes;
    using UnityEngine;

    internal class InterstitialAdInvoker : MonoBehaviour
    {
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        private void OnEnable() =>
            _services.Mediation.ShowInterstitialAd();
    }
}