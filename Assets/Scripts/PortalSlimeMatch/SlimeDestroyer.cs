using System.Collections.Generic;
using DG.Tweening;
using Model;
using UnityEngine;

namespace PortalSlimeMatch
{
    internal class SlimeDestroyer : MonoBehaviour
    {
        private static readonly Vector3 MinScale = Vector3.zero;

        [SerializeField] private Slime _slime;
        [SerializeField] private Transform _model;
        [SerializeField][Min(0.001f)] private float _animationDuration = 0.001f;
        [SerializeField] private List<Collider> _colliders = new ();

        private Sequence _sequence;

        private void OnEnable() =>
            _slime.Caught += Destroy;

        private void OnDisable()
        {
            _slime.Caught -= Destroy;
            _sequence.Kill(true);
        }

        private void Destroy()
        {
            _colliders.ForEach(element => element.enabled = false);
            _sequence = DOTween.Sequence();
            _sequence.Append(_model.DOScale(MinScale, _animationDuration).SetEase(Ease.Linear));
            _sequence.AppendCallback(() => Destroy(gameObject));
        }
    }
}