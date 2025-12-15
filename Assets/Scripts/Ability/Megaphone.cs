namespace Ability
{
    using Audio;
    using Bootstrap;
    using Currency;
    using Map;
    using Model;

    public class Megaphone : Ability
    {
        private Map _map;
        private Audio _audio;
        
        public Megaphone(AbilityData data)
            : base(data)
        { }

        public void Initialize(SavvyServicesProvider services, Sound sound, Money money, Map map)
        {
            Initialize(services, money);
            _audio = sound;
            _map = map;
        }

        protected override void Activate()
        {
            int portalCount = _map.Portals.Count;

            foreach (Slime slime in _map.Slimes)
            {
                slime.WakeUp(portalCount);
            }
            
            _audio.Play(AudioClipKey.Megaphone);
        }
    }
}