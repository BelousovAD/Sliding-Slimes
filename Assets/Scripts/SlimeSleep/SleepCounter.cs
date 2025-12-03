namespace SlimeSleep
{
    using System;
    using Countable;
    using Gameplay;
    using Model;
    using Reflex.Attributes;
    using UnityEngine;

    public class SleepCounter : MonoBehaviour, ICountable
    {
        [SerializeField] private Slime _slime;
        private int _count;
        private PortalCounter _portalCounter;

        public event Action CountChanged;
        
        public int Count
        {
            get
            {
                return _count;
            }

            private set
            {
                if (value != _count)
                {
                    _count = value < ICountable.MinCount ? ICountable.MinCount : value;
                    CountChanged?.Invoke();
                }
            }
        }

        public bool IsSleepActive => Count > ICountable.MinCount;

        [Inject]
        private void Initialize(PortalCounter portalCounter) =>
            _portalCounter = portalCounter;

        private void OnEnable() =>
            UpdateCount();

        private void Start()
        {
            _portalCounter.CountChanged += UpdateCount;
            UpdateCount();
        }

        private void OnDestroy() =>
            _portalCounter.CountChanged -= UpdateCount;

        private void UpdateCount()
        {
            if (_portalCounter is not null)
            {
                Count = _portalCounter.Count - _slime.PortalCountToAwake;
            }
        }
    }
}