namespace Gameplay
{
    using System;
    using Bootstrap;

    internal class Level
    {
        private const string SaveKey = nameof(Level);

        private SavvyServicesProvider _services;
        private int _available = 1;
        private int _chosen = 1;

        public Level(int max) =>
            Max = max;

        public event Action AvailableChanged;
        public event Action ChosenChanged;

        public int Available
        {
            get
            {
                return _available;
            }

            private set
            {
                if (value != _available)
                {
                    _available = value;
                    Save();
                    AvailableChanged?.Invoke();
                }
            }
        }

        public int Chosen
        {
            get
            {
                return _chosen;
            }

            private set
            {
                if (value != _chosen)
                {
                    _chosen = value;
                    ChosenChanged?.Invoke();
                }
            }
        }

        public int Max { get; }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        public void Load() =>
            Available = _services.Preferences.LoadInt(SaveKey, 1);

        private void Save() =>
            _services.Preferences.SaveInt(SaveKey, Available);
    }
}