namespace LuckyBlock
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ability;
    using Currency;
    using Model;
    using Reflex.Attributes;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class LuckyBlockReward : MonoBehaviour
    {
        private const float MinRandomValue = 0f;
        private const int MoneyFactor = 2;
        
        [SerializeField] private LuckyBlock _luckyBlock;
        [SerializeField] private HealthCounter _healthCounter;
        [SerializeField, Min(0)] private int _minMoney;
        [SerializeField, Min(0)] private int _maxMoney = 1;
        [Header("When Upgraded")]
        [SerializeField] private List<Chance> _bonusChances = new();
        
        private Money _money;
        private Hammer _hammer;
        private Hourglass _hourglass;
        private Lightning _lightning;
        private Megaphone _megaphone;
        private float _totalChance;

        [Inject]
        private void Initialize(
            Money money,
            Hammer hammer,
            Hourglass hourglass,
            Lightning lightning,
            Megaphone megaphone)
        {
            _money = money;
            _hammer = hammer;
            _hourglass = hourglass;
            _lightning = lightning;
            _megaphone = megaphone;
        }

        private void Awake() =>
            _totalChance = _bonusChances.Sum(chance => chance.Percent);

        private void OnEnable() =>
            _healthCounter.CountChanged += Earn;

        private void OnDisable() => 
            _healthCounter.CountChanged -= Earn;

        private void Earn()
        {
            if (_healthCounter.IsAlive)
            {
                return;
            }
            
            if (_luckyBlock.IsUpgraded == false)
            {
                _money.Earn(Random.Range(_minMoney, _maxMoney + 1));
            }
            else
            {
                float roll = Random.Range(MinRandomValue, _totalChance);
                float chanceSum = 0f;

                foreach (Chance chance in _bonusChances)
                {
                    chanceSum += chance.Percent;

                    if (roll > chanceSum)
                    {
                        continue;
                    }
                    
                    switch (chance.Bonus)
                    {
                        case BonusType.X2Money:
                            _money.Earn(Random.Range(_minMoney, _maxMoney + 1) * MoneyFactor);
                            break;
                        case BonusType.Hourglass:
                            _hourglass.Add();
                            break;
                        case BonusType.Megaphone:
                            _megaphone.Add();
                            break;
                        case BonusType.Lightning:
                            _lightning.Add();
                            break;
                        case BonusType.Hammer:
                            _hammer.Add();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                        
                    break;
                }
            }
        }

        private void OnValidate()
        {
            if (_maxMoney <= _minMoney)
            {
                _maxMoney = _minMoney + 1;
            }
        }

        private enum BonusType
        {
            X2Money = 0,
            Hourglass,
            Megaphone,
            Lightning,
            Hammer,
        }
        
        [Serializable]
        private struct Chance
        {
            public float Percent;
            public BonusType Bonus;
        }
    }
}
