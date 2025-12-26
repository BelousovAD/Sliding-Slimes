using Reflex.Core;
using Timer;
using UnityEngine;
using Window;

namespace Gameplay
{
    internal class GameplayInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private string _defeatWindowId;
        [SerializeField] private string _victoryWindowId;

        private ContainerBuilder _builder;
        private Gameplay _gameplay;
        private PortalCounter _portalCounter;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _gameplay = new Gameplay(_defeatWindowId, _victoryWindowId);
            _portalCounter = new PortalCounter();

            _builder.AddSingleton(_gameplay);
            _builder.AddSingleton(_portalCounter);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _gameplay.Initialize(
                container.Resolve<Level.Level>(),
                container.Resolve<PortalCounter>(),
                container.Resolve<CoroutineTimer>(),
                container.Resolve<IWindowService>());
            _portalCounter.Initialize(container.Resolve<Map.Map>());
        }
    }
}