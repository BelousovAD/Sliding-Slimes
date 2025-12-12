using Bootstrap;
using Common;
using Reflex.Attributes;
using Savvy.Interfaces;
using UnityEngine;

internal class SwitchLanguageButton : AbstractButton
{
    [SerializeField] private LocalizationSettings _settings;

    private int _index;
    private SavvyServicesProvider _services;

    [Inject]
    private void Initialize(SavvyServicesProvider servicesProvider)
    {
        _services = servicesProvider;
        SystemLanguage currentLanguage = _services.Localisation.GetLanguage();

        for (int i = 0; i < _settings.TranslationsData.Length; i++)
        {
            if (_settings.TranslationsData[i].SystemLanguage == currentLanguage)
            {
                _index = i;
            }
        }
    }

    protected override void HandleClick()
    {
        _index = (_index + 1) % _settings.TranslationsData.Length;
        _services.Localisation.SetLanguage(_settings.TranslationsData[_index].SystemLanguage);
    }
}