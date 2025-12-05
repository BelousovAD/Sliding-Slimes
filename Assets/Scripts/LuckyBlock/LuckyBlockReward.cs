namespace LuckyBlock
{
    using Currency;
    using Reflex.Attributes;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class LuckyBlockReward : MonoBehaviour
    {
        [SerializeField] private HealthCounter _healthCounter;
        [SerializeField, Min(0)] private int _minBoundary;
        [SerializeField, Min(0)] private int _maxBoundary = 1;
        
        private Money _money;

        [Inject]
        private void Initialize(Money money) =>
            _money = money;

        private void OnEnable() =>
            _healthCounter.CountChanged += Earn;

        private void OnDisable() => 
            _healthCounter.CountChanged -= Earn;

        private void Earn()
        {
            if (_healthCounter.IsAlive == false)
            {
                _money.Earn(Random.Range(_minBoundary, _maxBoundary + 1));
            }
        }

        private void OnValidate()
        {
            if (_maxBoundary <= _minBoundary)
            {
                _maxBoundary = _minBoundary + 1;
            }
        }
    }
}
