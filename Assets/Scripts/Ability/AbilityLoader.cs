namespace Ability
{
    using Bootstrap;
    using Reflex.Attributes;
    using UnityEngine;

    internal class AbilityLoader : MonoBehaviour, ILoadable
    {
        private Ability _hammer;
        private Ability _hourglass;
        private Ability _lightning;
        private Ability _megaphone;

        [Inject]
        private void Initialize(Hammer hammer,
            Hourglass hourglass,
            Lightning lightning,
            Megaphone megaphone)
        {
            _hammer = hammer;
            _hourglass = hourglass;
            _lightning = lightning;
            _megaphone = megaphone;
        }

        public void Load()
        {
            _hammer.Load();
            _hourglass.Load();
            _lightning.Load();
            _megaphone.Load();
        }
    }
}