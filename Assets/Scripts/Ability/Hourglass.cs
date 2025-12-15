namespace Ability
{
    using Audio;
    using Bootstrap;
    using Currency;
    using Timer;

    public class Hourglass : Ability
    {
        private const int AdditionalTime = 60;

        private Audio _audio;
        private CoroutineTimer _timer;
        
        public Hourglass(AbilityData data)
            : base(data)
        { }

        public void Initialize(
            SavvyServicesProvider servicesProvider,
            Sound sound,
            Money money,
            CoroutineTimer timer)
        {
            Initialize(servicesProvider, money);
            _audio = sound;
            _timer = timer;
        }

        protected override void Activate()
        {
            _audio.Play(AudioClipKey.Hourglass);
            _timer.Add(AdditionalTime);
        }
    }
}