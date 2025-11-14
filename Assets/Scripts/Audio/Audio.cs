namespace Audio
{
    using System;
    using Bootstrap;
    using UnityEngine;

    internal class Audio
    {
        private readonly SavvyServicesProvider _services;
        private readonly AudioType _type;
        private bool _isActive = true;
        private float _volume = 1f;

        public Audio(SavvyServicesProvider servicesProvider, AudioType type)
        {
            _services = servicesProvider;
            _type = type;
        }

        public event Action ActivityChanged;
        public event Action VolumeChanged;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            private set
            {
                _isActive = value;
                ActivityChanged?.Invoke();
            }
        }

        public float Volume
        {
            get
            {
                return _volume;
            }

            private set
            {
                _volume = value;
                VolumeChanged?.Invoke();
            }
        }

        public void SetActive(bool value)
        {
            IsActive = value;
            Save();
        }

        public void SetVolume(float value)
        {
            Volume = Mathf.Clamp01(value);
            Save();
        }

        public void Load()
        {
            IsActive = _services.Preferences.LoadBool(_type + nameof(IsActive), true);
            Volume = Mathf.Clamp01(_services.Preferences.LoadFloat(_type + nameof(Volume), 0.5f));
        }

        private void Save()
        {
            _services.Preferences.SaveBool(_type + nameof(IsActive), IsActive);
            _services.Preferences.SaveFloat(_type + nameof(Volume), Volume);
        }
    }
}