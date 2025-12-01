namespace Map
{
    using System;

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
        public event Action Caught;

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
        
        public bool IsCaught { get; private set; }

        public void Catch()
        {
            IsCaught = true;
            Caught?.Invoke();
        }
    }
}