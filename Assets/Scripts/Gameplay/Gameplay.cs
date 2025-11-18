namespace Gameplay
{
    using System;
    using Timer;
    using Window;

    internal class Gameplay : IDisposable
    {
        private readonly string _defeatWindowId;
        private readonly string _victoryWindowId;
        private CoroutineTimer _timer;
        private IWindowService _windowService;

        public Gameplay(string defeatWindowId, string victoryWindowId)
        {
            _defeatWindowId = defeatWindowId;
            _victoryWindowId = victoryWindowId;
        }

        public void Initialize(CoroutineTimer timer, IWindowService windowService)
        {
            _timer = timer;
            _windowService = windowService;

            _timer.TimeIsUp += OpenDefeatWindow;
        }

        public void Dispose() =>
            _timer.TimeIsUp -= OpenDefeatWindow;

        private void OpenDefeatWindow() =>
            _windowService.Open(_defeatWindowId, false);
    }
}