namespace Travelator
{
    using System.Collections.Generic;
    using System.Linq;
    using Audio;
    using Model;
    using Reflex.Attributes;
    using UnityEngine;
    using AudioType = Audio.AudioType;

    internal class TravelatorEffect : MonoBehaviour
    {
        private const AudioType SoundType = AudioType.Sound;
        
        [SerializeField] private Travelator _travelator;
        [SerializeField] private ParticleSystem _particleSystem;

        private Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio> audios) =>
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