using System;
using Countable;
using Model;
using UnityEngine;

namespace LuckyBlock
{
    internal class HealthCounter : MonoBehaviour, ICountable
    {
        [SerializeField] private Model.LuckyBlock _luckyBlock;

        private int _count;

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
                    _count = value < ICountable.MinCount ? ICountable.MinCount : value;
                    CountChanged?.Invoke();
                }
            }
        }

        public bool IsAlive => Count > ICountable.MinCount;

        private void Start() =>
            Count = _luckyBlock.StartHealth;

        private void OnEnable() =>
            _luckyBlock.DestroyRequested += Die;

        private void OnDisable() =>
            _luckyBlock.DestroyRequested -= Die;

        private void OnCollisionEnter(Collision other)
        {
            if (IsAlive && other.gameObject.TryGetComponent(out Slime _))
            {
                Count--;
            }
        }

        private void Die() =>
            Count = ICountable.MinCount;
    }
}