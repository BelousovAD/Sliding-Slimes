namespace Ability
{
    using Bootstrap;
    using Currency;
    using Map;
    using Model;

    public class Hammer : Ability
    {
        private Map _map;
        
        public Hammer(AbilityData data)
            : base(data)
        { }

        public void Initialize(SavvyServicesProvider services, Money money, Map map)
        {
            Initialize(services, money);
            _map = map;
        }

        protected override void Activate()
        {
            foreach (LuckyBlock luckyBlock in _map.LuckyBlocks)
            {
                luckyBlock.Upgrade();
                luckyBlock.Destroy();
            }
        }
    }
}