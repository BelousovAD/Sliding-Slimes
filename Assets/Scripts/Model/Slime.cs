namespace Model
{
    using System;
    using Countable;
    using SlimeTypeable;

    public class Slime : AbstractModel, ICountable, ISlimeTypeable
    {
        private const int MinPortalCountToAwake = 1;
        
        private int _remainingCount;
        private int _countToAwake;

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

        public SlimeType Type { get; private set; }
        
        public bool IsCaught { get; private set; }

        public bool IsSleeping => Count > ICountable.MinCount;
        
        public void Initialize(SlimeType type, int portalCountToAwake)
        {
            Type = type;

            if (portalCountToAwake < MinPortalCountToAwake)
            {
                throw new ArgumentOutOfRangeException(nameof(portalCountToAwake),
                    $"Must be greater than or equal to {MinPortalCountToAwake}");
            }

            _countToAwake = portalCountToAwake;
        }

        public void Catch()
        {
            IsCaught = true;
            Caught?.Invoke();
        }
    }
}