namespace Travelator
{
    using Model;
    using UnityEngine;

    internal class Animator : MonoBehaviour
    {
        [SerializeField] private Travelator _travelator;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable() =>
            _travelator.ManualStatusChanged += PlayAnimation;

        private void OnDisable() =>
            _travelator.ManualStatusChanged -= PlayAnimation;

        private void PlayAnimation() =>
            _particleSystem.Play();
    }
}