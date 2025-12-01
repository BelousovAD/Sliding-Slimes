namespace Map
{
    using System;
    using System.Collections.Generic;

    public class PortalCounter : ICountable, IDisposable
    {
        private const int Min = 0;
        
        private readonly Map _map;
        private readonly List<Portal> _portals = new();
        private int _remainingCount;

        public PortalCounter(Map map) =>
            _map = map;
        
        public event Action CountChanged;

        public int Count
        {
            get
            {
                return _remainingCount;
            }

            private set
            {
                if (value == _remainingCount)
                {
                    return;
                }
                
                _remainingCount = value < Min ? Min : value;
                CountChanged?.Invoke();
            }
        }

        public void Initialize()
        {
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