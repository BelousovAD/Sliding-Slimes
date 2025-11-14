namespace Audio
{
    using Bootstrap;

    internal class Sound : Audio
    {
        public Sound(SavvyServicesProvider servicesProvider)
            : base(servicesProvider, AudioType.Sound)
        { }
    }
}