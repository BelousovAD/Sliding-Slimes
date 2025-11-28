namespace Map
{
    using System;
    using UnityEngine;

    internal class Slime : AbstractModel, ICountable, ISlimeTypeable
    {
        private int _count;

        public Slime(SlimeType type, int sleepCount)
        {
            Type = type;

            if (sleepCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sleepCount), "Must be positive");
            }

            Count = sleepCount;
        }
        
        public event Action CountChanged;
        public event Action<Slime> Caught;

        public int Count
        {
            get
            {
                return _count;
            }

            private set
            {
                if (value == _count)
                {
                    return;
                }
                
                _count = value < ICountable.MinCount ? ICountable.MinCount : value;
                CountChanged?.Invoke();
            }
        }

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
        
        public bool IsCaught { get; private set; }

        public void Catch()
        {
            IsCaught = true;
            Caught?.Invoke(this);
        }
    }
}