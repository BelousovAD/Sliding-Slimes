using System;
using Bootstrap;
using Currency;
using UnityEngine;

namespace Ability
{
    public abstract class Ability
    {
        public const int MinCount = 0;
        private const int CountForAction = 1;

        private readonly AbilityData _data;
        private int _count;
        private Money _money;
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
        public event Action Used;

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

        public bool IsOnlyRewardForAd => _data.IsOnlyRewardForAd;

        public int Price => _data.Price;

        public int UnlockLevel => _data.UnlockLevel;

        public void Initialize(SavvyServicesProvider servicesProvider, Money money)
        {
            _services = servicesProvider;
            _money = money;
        }

        public void Add() =>
            Count += CountForAction;

        public void Use()
        {
            if (Count >= CountForAction)
            {
                Count -= CountForAction;
                Activate();
                Used?.Invoke();
                return;
            }

            if (IsOnlyRewardForAd || _money.TrySpend(Price) == false)
            {
                _services.Mediation.ShowRewardedAd(() =>
                {
                    Activate();
                    Used?.Invoke();
                });
                return;
            }

            Activate();
            Used?.Invoke();
        }

        public void Load() =>
            Count = _services.Preferences.LoadInt(_data.SaveKey, _data.StartCount);

        protected abstract void Activate();

        private void Save() =>
            _services.Preferences.SaveInt(_data.SaveKey, Count);
    }
}