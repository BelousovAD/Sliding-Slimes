namespace Audio
{
    using Bootstrap;
    
    internal class Music : Audio
    {
        public Music(SavvyServicesProvider servicesProvider)
            : base(servicesProvider, AudioType.Music)
        { }
    }
}