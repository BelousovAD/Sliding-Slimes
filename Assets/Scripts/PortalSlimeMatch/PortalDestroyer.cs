namespace PortalSlimeMatch
{
    using DG.Tweening;
    using Model;
    using UnityEngine;

    internal class PortalDestroyer : MonoBehaviour
    {
        [SerializeField] private Portal _portal;
        [SerializeField] private Transform _model;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField, Min(0.001f)] private float _animationDuration = 0.001f;
        [SerializeField, Min(0.001f)] private float _particleSystemDuration = 0.001f;

        private void OnEnable() =>
            _portal.SlimeCaught += Destroy;

        private void OnDisable() =>
            _portal.SlimeCaught -= Destroy;

        private void Destroy()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_model.DOScale(Vector3.zero, _animationDuration).SetEase(Ease.Linear));
            sequence.AppendCallback(() => _particleSystem.Play());
            sequence.AppendInterval(_particleSystemDuration);
            sequence.AppendCallback(() => Destroy(gameObject));
            sequence.SetUpdate(true);
        }
    }
}