namespace PortalSlimeMatch
{
    using DG.Tweening;
    using Model;
    using UnityEngine;

    internal class SlimeDestroyer : MonoBehaviour
    {
        [SerializeField] private Slime _slime;
        [SerializeField] private Transform _model;
        [SerializeField, Min(0.001f)] private float _animationDuration = 0.001f;
        
        private void OnEnable() =>
            _slime.Caught += Destroy;

        private void OnDisable() =>
            _slime.Caught -= Destroy;

        private void Destroy()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_model.DOScale(Vector3.zero, _animationDuration).SetEase(Ease.Linear));
            sequence.AppendCallback(() => Destroy(gameObject));
            sequence.SetUpdate(true);
        }
    }
}