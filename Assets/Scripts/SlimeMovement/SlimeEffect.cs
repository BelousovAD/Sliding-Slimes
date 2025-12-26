using System.Collections.Generic;
using System.Linq;
using Audio;
using DG.Tweening;
using Reflex.Attributes;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace SlimeMovement
{
    internal class SlimeEffect : MonoBehaviour
    {
        private const AudioType SoundType = AudioType.Sound;

        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Transform _model;
        [SerializeField] private Vector3 _scalePunch;
        [SerializeField][Min(0.001f)] private float _animationDuration = 0.001f;

        private Tweener _tweener;
        private Audio.Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio.Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);

        private void OnEnable() =>
            _inputReader.MoveRequested += PlayAnimation;

        private void OnDisable()
        {
            _inputReader.MoveRequested -= PlayAnimation;
            _tweener.Kill(true);
        }

        private void PlayAnimation(Vector2Int direction)
        {
            if (_tweener.IsActive())
            {
                _tweener.Kill(true);
            }

            _audio.Play(AudioClipKey.Slime);
            _tweener = _model.DOPunchScale(_scalePunch, _animationDuration);
        }
    }
}