namespace Gameplay
{
    using System;
    using Level;
    using Timer;
    using Window;

    internal class Gameplay : IDisposable
    {
        private readonly string _defeatWindowId;
        private readonly string _victoryWindowId;
        private Level _level;
        private CoroutineTimer _timer;
        private IWindowService _windowService;

        public Gameplay(string defeatWindowId, string victoryWindowId)
        {
            _defeatWindowId = defeatWindowId;
            _victoryWindowId = victoryWindowId;
        }

        public void Initialize(Level level, CoroutineTimer timer, IWindowService windowService)
        {
            _level = level;
            _timer = timer;
            _windowService = windowService;

            _timer.TimeIsUp += OpenDefeatWindow;
        }

        public void Dispose() =>
            _timer.TimeIsUp -= OpenDefeatWindow;

        public void HandleVictory()
        {
            _level.Unlock();
            _windowService.Open(_victoryWindowId, false);
        }

        public void OpenDefeatWindow() =>
            _windowService.Open(_defeatWindowId, false);
    }
}