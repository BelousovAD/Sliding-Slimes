namespace Map
{
    using System;

    internal class LuckyBlock : AbstractModel, ICountable
    {
        private int _remainingCount;

        public LuckyBlock(int health)
        {
            if (health < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(health), "Must be greater than 1");
            }

            Count = health;
        }

        public event Action CountChanged;

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

        public void CountDown() =>
            Count--;
    }
}