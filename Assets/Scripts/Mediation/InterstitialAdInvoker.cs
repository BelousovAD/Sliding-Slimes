namespace Mediation
{
    using MirraGames.SDK;
    using UnityEngine;

    internal class InterstitialAdInvoker : MonoBehaviour
    {
        private void OnEnable() =>
            MirraSDK.Ads.InvokeInterstitial();
    }
}