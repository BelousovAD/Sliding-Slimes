namespace Model
{
    using System;
    using SlimeTypeable;

    public class Slime : AbstractModel, ISlimeTypeable
    {
        private const int MinPortalCountToAwake = 1;

        public event Action Caught;

        public SlimeType Type { get; private set; }

        public bool IsCaught { get; private set; }

        public int PortalCountToAwake { get; private set; } = MinPortalCountToAwake;

        public void Initialize(SlimeType type, int portalCountToAwake)
        {
            Type = type;

            if (portalCountToAwake < MinPortalCountToAwake)
            {
                throw new ArgumentOutOfRangeException(nameof(portalCountToAwake),
                    $"Must be greater than or equal to {MinPortalCountToAwake}");
            }

            PortalCountToAwake = portalCountToAwake;
        }

        public void Catch()
        {
            IsCaught = true;
            Caught?.Invoke();
        }
    }
}