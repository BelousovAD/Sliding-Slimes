namespace Travelator
{
    using Audio;
    using Model;
    using Reflex.Attributes;
    using UnityEngine;

    internal class TravelatorEffect : MonoBehaviour
    {
        [SerializeField] private Travelator _travelator;
        [SerializeField] private ParticleSystem _particleSystem;

        private Audio _audio;

        [Inject]
        private void Initialize(Sound sound) =>
            _audio = sound;

        private void OnEnable() =>
            _travelator.ManualStatusChanged += PlayAnimation;

        private void OnDisable() =>
            _travelator.ManualStatusChanged -= PlayAnimation;

        private void PlayAnimation()
        {
            _audio.Play(AudioClipKey.Lightning);
            _particleSystem.Play();
        }
    }
}