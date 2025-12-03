namespace Gameplay
{
    using System;
    using System.Collections.Generic;
    using Countable;
    using Map;
    using Model;

    internal class PortalCounter : ICountable, IDisposable
    {
        private readonly List<Portal> _portals = new();
        private Map _map;
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
        
        public void Initialize(Map map)
        {
            _map = map;
            _map.Initialized += Initialize;
            Initialize();
        }

        private void Initialize()
        {
            if (_map.IsInitialized == false)
            {
                return;
            }

            _map.Initialized -= Initialize;
            
            for (int x = 0; x < _map.Size.x; x++)
            {
                for (int y = 0; y < _map.Size.y; y++)
                {
                    foreach (AbstractModel model in _map[x, y])
                    {
                        if (model is not Portal portal)
                        {
                            continue;
                        }
                        
                        portal.SlimeCaught += CountDown;
                        _portals.Add(portal);
                    }
                }
            }

            Count = _portals.Count;
        }

        public void Dispose()
        {
            foreach (Portal portal in _portals)
            {
                portal.SlimeCaught -= CountDown;
            }
            
            _portals.Clear();
        }

        private void CountDown() =>
            Count--;
    }
}