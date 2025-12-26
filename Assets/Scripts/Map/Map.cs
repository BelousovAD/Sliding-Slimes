using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Map
{
    public class Map
    {
        private List<LuckyBlock> _luckyBlocks;
        private List<Portal> _portals;
        private List<Slime> _slimes;
        private List<Travelator> _travelators;

        public event Action Initialized;

        public bool IsInitialized { get; private set; }

        public Vector2Int Size { get; private set; }

        public IReadOnlyCollection<LuckyBlock> LuckyBlocks => _luckyBlocks;

        public IReadOnlyCollection<Portal> Portals => _portals;

        public IReadOnlyCollection<Slime> Slimes => _slimes;

        public IReadOnlyCollection<Travelator> Travelators => _travelators;

        public void Initialize(
            Vector2Int size,
            List<LuckyBlock> luckyBlocks,
            List<Portal> portals,
            List<Slime> slimes,
            List<Travelator> travelators)
        {
            Size = size;
            _luckyBlocks = luckyBlocks;
            _portals = portals;
            _slimes = slimes;
            _travelators = travelators;
            IsInitialized = true;
            Initialized?.Invoke();
        }
    }
}