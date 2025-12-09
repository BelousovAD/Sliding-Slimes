namespace Ability
{
    using Bootstrap;
    using Currency;
    using Timer;

    public class Hourglass : Ability
    {
        private const int AdditionalTime = 60;
        
        private CoroutineTimer _timer;
        
        public Hourglass(AbilityData data)
            : base(data)
        { }

        public void Initialize(SavvyServicesProvider servicesProvider, Money money, CoroutineTimer timer)
        {
            Initialize(servicesProvider, money);
            _timer = timer;
        }

        protected override void Activate() =>
            _timer.Add(AdditionalTime);
    }
}