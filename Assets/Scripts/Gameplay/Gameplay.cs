namespace Gameplay
{
    using System;
    using Level;
    using Map;
    using Timer;
    using Window;

    internal class Gameplay : IDisposable
    {
        private const int PortalCountToWin = 0;
        
        private readonly string _defeatWindowId;
        private readonly string _victoryWindowId;
        private Level _level;
        private PortalCounter _portalCounter;
        private CoroutineTimer _timer;
        private IWindowService _windowService;

        public Gameplay(string defeatWindowId, string victoryWindowId)
        {
            _defeatWindowId = defeatWindowId;
            _victoryWindowId = victoryWindowId;
        }

        public void Initialize(
            Level level,
            PortalCounter portalCounter,
            CoroutineTimer timer,
            IWindowService windowService)
        {
            _level = level;
            _portalCounter = portalCounter;
            _timer = timer;
            _windowService = windowService;

            _portalCounter.CountChanged += Victory;
            _timer.TimeIsUp += OpenDefeatWindow;
        }

        public void Dispose()
        {
            _portalCounter.CountChanged -= Victory;
            _timer.TimeIsUp -= OpenDefeatWindow;
        }

        public void Victory()
        {
            if (_portalCounter.Count != PortalCountToWin)
            {
                return;
            }
            
            if (_level.Chosen == _level.Available)
            {
                _level.Unlock();
            }

            _windowService.Open(_victoryWindowId, false);
        }

        public void OpenDefeatWindow() =>
            _windowService.Open(_defeatWindowId, false);
    }
}