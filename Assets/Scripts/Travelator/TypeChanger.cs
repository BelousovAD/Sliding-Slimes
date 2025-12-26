namespace Travelator
{
    using System.Collections.Generic;
    using System.Linq;
    using Audio;
    using Bootstrap;
    using Model;
    using Reflex.Attributes;
    using Timer;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using AudioType = Audio.AudioType;

    [RequireComponent(typeof(Collider))]
    public class TypeChanger : MonoBehaviour, IPointerClickHandler
    {
        private const AudioType SoundType = AudioType.Sound;
        
        [SerializeField] private Travelator _travelator;
        [SerializeField, Min(0)] private int _delay = 1;

        private CoroutineTimer _timer;
        private bool _isCached;
        private Audio _audio;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider, IEnumerable<Audio> audios)
        {
            _timer.Initialize(servicesProvider);
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);
        }

        private void Awake() =>
            _timer = new CoroutineTimer(_delay);

        private void Start()
        {
            _timer.TimeIsUp += ChangeType;
            _travelator.ManualStatusChanged += StopTimer;
            ChangeType();
        }

        private void OnDestroy()
        {
            _timer.TimeIsUp -= ChangeType;
            _travelator.ManualStatusChanged -= StopTimer;
            _timer.Stop();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_travelator.IsManual)
            {
                _audio.Play(AudioClipKey.Travelator);
                ChangeType();
            }
        }

        private void StopTimer() =>
            _timer.Stop();

        private void ChangeType()
        {
            _travelator.NextType();

            if (_travelator.IsManual == false)
            {
                _timer.Add(_delay);
            }
        }
    }
}