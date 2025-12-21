namespace Model
{
    using System;
    using SlimeTypeable;

    public class Portal : AbstractModel, ISlimeTypeable
    {
        public event Action SlimeCaught;

        public SlimeType Type { get; private set; }

        public bool HasSlime { get; private set; }
        
        public void Initialize(SlimeType type) =>
            Type = type;

        public void CatchSlime()
        {
            HasSlime = true;
            SlimeCaught?.Invoke();
        }
    }
}