namespace Model
{
    using System;
    using SlimeTypeable;

    public class Slime : AbstractModel, ISlimeTypeable
    {
        private const int MinPortalCountToAwake = 1;

        private int _portalCountToAwake = MinPortalCountToAwake;

        public event Action Caught;
        public event Action PortalCountToAwakeChanged;

        public SlimeType Type { get; private set; }

        public bool IsCaught { get; private set; }

        public int PortalCountToAwake
        {
            get
            {
                return _portalCountToAwake;
            }

            private set
            {
                if (value != _portalCountToAwake)
                {
                    _portalCountToAwake = value < MinPortalCountToAwake ? MinPortalCountToAwake : value;
                    PortalCountToAwakeChanged?.Invoke();
                }
            }
        }

        public void Initialize(SlimeType type, int portalCountToAwake)
        {
            Type = type;

            if (portalCountToAwake < MinPortalCountToAwake)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(portalCountToAwake),
                    $"Must be greater than or equal to {MinPortalCountToAwake}");
            }

            PortalCountToAwake = portalCountToAwake;
        }

        public void Catch()
        {
            IsCaught = true;
            Caught?.Invoke();
        }

        public void WakeUp(int portalCount) =>
            PortalCountToAwake = portalCount;
    }
}