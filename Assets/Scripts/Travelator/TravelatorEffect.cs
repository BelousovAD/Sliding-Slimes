using System.Collections.Generic;
using System.Linq;
using Audio;
using Reflex.Attributes;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Travelator
{
    internal class TravelatorEffect : MonoBehaviour
    {
        private const AudioType SoundType = AudioType.Sound;

        [SerializeField] private Model.Travelator _travelator;
        [SerializeField] private ParticleSystem _particleSystem;

        private Audio.Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio.Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);

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