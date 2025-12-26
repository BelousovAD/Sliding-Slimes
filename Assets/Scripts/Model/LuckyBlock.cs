using System;

namespace Model
{
    public class LuckyBlock : AbstractModel
    {
        private const int MinStartHealth = 1;

        public event Action DestroyRequested;

        public int StartHealth { get; private set; } = MinStartHealth;

        public bool IsUpgraded { get; private set; }

        public void Initialize(int health)
        {
            if (health < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(health),
                    $"Must be greater than {MinStartHealth}");
            }

            StartHealth = health;
        }

        public void Destroy() =>
            DestroyRequested?.Invoke();

        public void Upgrade() =>
            IsUpgraded = true;
    }
}