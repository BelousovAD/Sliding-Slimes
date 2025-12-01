namespace Map
{
    using System;

    internal class Portal : AbstractModel, ISlimeTypeable
    {
        public Portal(SlimeType type) =>
            Type = type;

        public event Action SlimeCaught;

        public SlimeType Type { get; }

        public bool HasSlime { get; private set; }

        public void CatchSlime()
        {
            HasSlime = true;
            SlimeCaught?.Invoke();
        }
    }
}