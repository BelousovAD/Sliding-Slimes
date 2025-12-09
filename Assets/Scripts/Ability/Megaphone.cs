namespace Ability
{
    using Bootstrap;
    using Currency;
    using Map;
    using Model;

    public class Megaphone : Ability
    {
        private Map _map;
        
        public Megaphone(AbilityData data)
            : base(data)
        { }

        public void Initialize(SavvyServicesProvider services, Money money, Map map)
        {
            Initialize(services, money);
            _map = map;
        }

        protected override void Activate()
        {
            int portalCount = _map.Portals.Count;

            foreach (Slime slime in _map.Slimes)
            {
                slime.WakeUp(portalCount);
            }
        }
    }
}