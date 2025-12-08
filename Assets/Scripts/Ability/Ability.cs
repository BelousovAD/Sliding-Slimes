namespace Ability
{
    using System;
    using Bootstrap;
    using UnityEngine;

    public abstract class Ability
    {
        public const int MinCount = 0;

        private readonly AbilityData _data;
        private int _count;
        private SavvyServicesProvider _services;

        public Ability(AbilityData data)
        {
            if (string.IsNullOrEmpty(data.SaveKey))
            {
                throw new ArgumentException(nameof(data.SaveKey), $"Can not be null or empty");
            }

            _data = data;
        }

        public event Action CountChanged;

        public int Count
        {
            get
            {
                return _count;
            }

            private set
            {
                if (value != _count)
                {
                    _count = value < MinCount ? MinCount : value;
                    Save();
                    CountChanged?.Invoke();
                }
            }
        }

        public Sprite Icon => _data.Icon;

        public bool IsRewardForAd => _data.IsRewardForAd;

        public int Price => _data.Price;

        public int UnlockLevel => _data.UnlockLevel;
        
        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        public void Earn(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Can not be negative");
            }

            Count += amount;
        }

        public bool TrySpend(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Can not be negative");
            }

            if (Count < amount)
            {
                return false;
            }
            
            Count -= amount;
            return true;
        }
        
        public void Load() =>
            Count = _services.Preferences.LoadInt(_data.SaveKey, _data.StartCount);
        
        private void Save() =>
            _services.Preferences.SaveInt(_data.SaveKey, Count);
    }
}