namespace Ability
{
    using Bootstrap;
    using Currency;
    using Map;
    using Model;

    public class Lightning : Ability
    {
        private Map _map;
        
        public Lightning(AbilityData data)
            : base(data)
        { }

        public void Initialize(SavvyServicesProvider services, Money money, Map map)
        {
            Initialize(services, money);
            _map = map;
        }

        protected override void Activate()
        {
            foreach (Travelator travelator in _map.Travelators)
            {
                travelator.SetManualControl();
            }
        }
    }
}