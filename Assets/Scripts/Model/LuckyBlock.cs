namespace Model
{
    using System;

    public class LuckyBlock : AbstractModel
    {
        private const int MinStartHealth = 1;

        public int StartHealth { get; private set; } = MinStartHealth;
        
        public void Initialize(int health)
        {
            if (health < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(health),
                    $"Must be greater than {MinStartHealth}");
            }

            StartHealth = health;
        }
    }
}