namespace Gameplay
{
    using System;
    using System.Collections.Generic;
    using Countable;
    using Map;
    using Model;

    public class PortalCounter : ICountable, IDisposable
    {
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
        }

        private void Initialize()
        {
            foreach (Portal portal in _map.Portals)
            {
                portal.SlimeCaught += CountDown;
            }

            Count = _map.Portals.Count;
        }

        public void Dispose()
        {
            foreach (Portal portal in _map.Portals)
            {
                portal.SlimeCaught -= CountDown;
            }
        }

        private void CountDown() =>
            Count--;
    }
}