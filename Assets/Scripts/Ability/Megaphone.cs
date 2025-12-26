using Audio;
using Bootstrap;
using Currency;
using Model;

namespace Ability
{
    public class Megaphone : Ability
    {
        private Map.Map _map;
        private Audio.Audio _audio;

        public Megaphone(AbilityData data)
            : base(data)
        { }

        public void Initialize(SavvyServicesProvider services, Audio.Audio sound, Money money, Map.Map map)
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