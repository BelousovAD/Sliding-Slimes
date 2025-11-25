namespace Map
{
    using System;

    internal class LuckyBlock : AbstractModel
    {
        public LuckyBlock(int health)
        {
            if (health < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(health), "Must be greater than 1");
            }

            Health = health;
        }

        public int Health { get; }
    }
}