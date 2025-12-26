using Bootstrap;
using Currency;
using Model;

namespace Ability
{
    public class Hammer : Ability
    {
        private Map.Map _map;

        public Hammer(AbilityData data)
            : base(data)
        { }

        public void Initialize(SavvyServicesProvider services, Money money, Map.Map map)
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