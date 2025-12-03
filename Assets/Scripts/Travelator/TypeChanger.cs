namespace Travelator
{
    using Bootstrap;
    using Model;
    using Reflex.Attributes;
    using Timer;
    using UnityEngine;

    public class TypeChanger : MonoBehaviour
    {
        [SerializeField] private Travelator _travelator;
        [SerializeField, Min(0)] private int _delay = 1;

        private CoroutineTimer _timer;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _timer.Initialize(servicesProvider);

        private void Awake() =>
            _timer = new CoroutineTimer(_delay);

        private void Start()
        {
            _timer.TimeIsUp += ChangeType;
            ChangeType();
        }

        private void OnDestroy()
        {
            _timer.TimeIsUp -= ChangeType;
            _timer.Stop();
        }

        private void ChangeType()
        {
            _travelator.NextType();
            _timer.Add(_delay);
        }
    }
}