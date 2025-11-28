namespace Map
{
    using System;
    using UnityEngine;

    internal class Portal : AbstractModel, ISlimeTypeable
    {
        public Portal(SlimeType type) =>
            Type = type;

        public event Action<Portal> SlimeCaught;

        public SlimeType Type { get; }

        public Color Color
        {
            get
            {
                return Type switch
                {
                    SlimeType.Blue => new Color(0f, 0.5f, 1f),
                    SlimeType.Grey => Color.grey,
                    SlimeType.Orange => new Color(1f, 0.5f, 0f),
                    SlimeType.Purple => Color.magenta,
                    SlimeType.Red => Color.red,
                    SlimeType.White => Color.white,
                    SlimeType.Yellow => Color.yellow,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public bool HasSlime { get; private set; }

        public void CatchSlime()
        {
            HasSlime = true;
            SlimeCaught?.Invoke(this);
        }
    }
}