namespace Bootstrap
{
    using Savvy.Container;
    using Savvy.Interfaces;

    internal class SavvyServicesProvider : NetSavvy
    {
        private ILocalizationService _localization;
        private IMediationService _mediation;
        private IPreferencesService _preferences;
        
        public ILocalizationService Localisation =>
            _localization ??= GetService<ILocalizationService>();

        public IMediationService Mediation =>
            _mediation ??= GetService<IMediationService>();

        public IPreferencesService Preferences =>
            _preferences ??= GetService<IPreferencesService>();
    }
}