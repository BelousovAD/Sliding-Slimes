namespace Map
{
    using System;

    public class Slime : AbstractModel, ICountable, IDisposable, ISlimeTypeable
    {
        private const int MinPortalCountToAwake = 1;
        
        private int _remainingCount;
        private readonly int _countToAwake;
        private readonly ICountable _portalCounter;

        public Slime(SlimeType type, int portalCountToAwake, ICountable portalCounter)
        {
            Type = type;

            if (portalCountToAwake < MinPortalCountToAwake)
            {
                throw new ArgumentOutOfRangeException(nameof(portalCountToAwake),
                    $"Must be greater than or equal to {MinPortalCountToAwake}");
            }

            _countToAwake = portalCountToAwake;
            _portalCounter = portalCounter;

            _portalCounter.CountChanged += UpdateRemainingCount;
        }

        public event Action CountChanged;
        public event Action Caught;

        public int Count
        {
            get
            {
                return _remainingCount;
            }

            private set
            {
                if (value == _remainingCount)
                {
                    return;
                }
                
                _remainingCount = value < ICountable.MinCount ? ICountable.MinCount : value;
                CountChanged?.Invoke();
            }
        }

        public SlimeType Type { get; }
        
        public bool IsCaught { get; private set; }

        public bool IsSleeping => Count > ICountable.MinCount;

        public void Dispose() =>
            _portalCounter.CountChanged -= UpdateRemainingCount;

        public void Catch()
        {
            IsCaught = true;
            Caught?.Invoke();
        }

        private void UpdateRemainingCount() =>
            Count = _portalCounter.Count - _countToAwake;
    }
}