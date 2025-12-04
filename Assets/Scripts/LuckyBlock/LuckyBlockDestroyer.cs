namespace LuckyBlock
{
    using UnityEngine;

    internal class LuckyBlockDestroyer : MonoBehaviour
    {
        [SerializeField] private HealthCounter _healthCounter;

        private void OnEnable() =>
            _healthCounter.CountChanged += Destroy;

        private void OnDisable() =>
            _healthCounter.CountChanged -= Destroy;

        private void Destroy()
        {
            if (_healthCounter.IsAlive == false)
            {
                Destroy(gameObject);
            }
        }
    }
}