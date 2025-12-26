using System.Collections.Generic;
using System.Linq;
using Audio;
using DG.Tweening;
using Reflex.Attributes;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace LuckyBlock
{
    internal class LuckyBlockDestroyer : MonoBehaviour
    {
        private static readonly Vector3 MinScale = Vector3.zero;
        private const AudioType SoundType = AudioType.Sound;

        [SerializeField] private HealthCounter _healthCounter;
        [SerializeField] private Transform _model;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Vector3 _scalePunch;
        [SerializeField][Min(0.001f)] private float _animationDuration = 0.001f;
        [SerializeField] private List<Collider> _colliders = new ();

        private Sequence _sequence;
        private Audio.Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio.Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);

        private void OnEnable() =>
            _healthCounter.CountChanged += Destroy;

        private void OnDisable()
        {
            _healthCounter.CountChanged -= Destroy;
            _sequence.Kill(true);
        }

        private void Destroy()
        {
            if (_sequence.IsActive())
            {
                _sequence.Kill(true);
            }

            _sequence = DOTween.Sequence();

            if (_healthCounter.IsAlive)
            {
                _sequence.AppendCallback(() => _audio.Play(AudioClipKey.LuckyBlock));
                _sequence.Append(_model.DOPunchScale(_scalePunch, _animationDuration));
            }
            else
            {
                _colliders.ForEach(element => element.enabled = false);
                _sequence.Append(_model.DOScale(MinScale, _animationDuration));
                _sequence.AppendCallback(() =>
                {
                    _audio.Play(AudioClipKey.LuckyBlock);
                    _particleSystem.Play();
                });
                _sequence.AppendInterval(_particleSystem.main.duration);
                _sequence.AppendCallback(() => Destroy(gameObject));
            }
        }
    }
}