namespace PortalSlimeMatch
{
    using DG.Tweening;
    using Model;
    using UnityEngine;

    internal class PortalDestroyer : MonoBehaviour
    {
        private static readonly Vector3 MinScale = Vector3.zero;

        [SerializeField] private Portal _portal;
        [SerializeField] private Transform _model;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField, Min(0.001f)] private float _animationDuration = 0.001f;

        private Sequence _sequence;

        private void OnEnable() =>
            _portal.SlimeCaught += Destroy;

        private void OnDisable()
        {
            _portal.SlimeCaught -= Destroy;
            _sequence.Kill(true);
        }

        private void Destroy()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(_model.DOScale(MinScale, _animationDuration).SetEase(Ease.Linear));
            _sequence.AppendCallback(() => _particleSystem.Play());
            _sequence.AppendInterval(_particleSystem.main.duration);
            _sequence.AppendCallback(() => Destroy(gameObject));
        }
    }
}