namespace PortalSlimeMatch
{
    using System.Collections.Generic;
    using System.Linq;
    using Audio;
    using DG.Tweening;
    using Model;
    using Reflex.Attributes;
    using UnityEngine;
    using AudioType = Audio.AudioType;

    internal class PortalDestroyer : MonoBehaviour
    {
        private static readonly Vector3 MinScale = Vector3.zero;
        private const AudioType SoundType = AudioType.Sound;

        [SerializeField] private Portal _portal;
        [SerializeField] private Transform _model;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField][Min(0.001f)] private float _animationDuration = 0.001f;
        [SerializeField] private List<Collider> _colliders = new ();

        private Sequence _sequence;
        private Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);

        private void OnEnable() =>
            _portal.SlimeCaught += Destroy;

        private void OnDisable()
        {
            _portal.SlimeCaught -= Destroy;
            _sequence.Kill(true);
        }

        private void Destroy()
        {
            _colliders.ForEach(element => element.enabled = false);
            _sequence = DOTween.Sequence();
            _sequence.Append(_model.DOScale(MinScale, _animationDuration).SetEase(Ease.Linear));
            _sequence.AppendCallback(() =>
            {
                _audio.Play(AudioClipKey.Portal);
                _particleSystem.Play();
            });
            _sequence.AppendInterval(_particleSystem.main.duration);
            _sequence.AppendCallback(() => Destroy(gameObject));
        }
    }
}