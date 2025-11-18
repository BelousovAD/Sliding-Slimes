namespace Timer
{
    using Bootstrap;
    using Reflex.Core;
    using UnityEngine;

    internal class TimerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField, Min(1)] private int _startTime = 90;

        private CoroutineTimer _timer;
        private ContainerBuilder _builder;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _timer = new CoroutineTimer(_startTime);

            _builder.AddSingleton(_timer);
            
            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;
            
            _timer.Initialize(container.Resolve<SavvyServicesProvider>());
            _timer.TryAdd(_startTime);
        }
    }
}