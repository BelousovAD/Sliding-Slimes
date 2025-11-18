namespace Gameplay
{
    using System;
    using Bootstrap;

    internal class Level
    {
        private const int Min = 1;
        private const string SaveKey = nameof(Level);

        private SavvyServicesProvider _services;
        private int _available = Min;
        private int _chosen = Min;

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

        public void Choose(int number)
        {
            if (number < Min)
            {
                throw new ArgumentOutOfRangeException(nameof(number), $"Can't be less than {Min}");
            }

            if (number > Available || number > Max)
            {
                throw new ArgumentOutOfRangeException(nameof(number), $"Can't be greater than {Available} or {Max}");
            }

            Chosen = number;
        }

        public void Unlock()
        {
            if (Available <= Max)
            {
                Available++;
            }
        }
        
        public void Load() =>
            Available = _services.Preferences.LoadInt(SaveKey, Min);

        private void Save() =>
            _services.Preferences.SaveInt(SaveKey, Available);
    }
}