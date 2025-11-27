namespace Gameplay
{
    using Level;
    using Map;
    using Reflex.Core;
    using Timer;
    using UnityEngine;
    using Window;

    internal class GameplayInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private string _defeatWindowId;
        [SerializeField] private string _victoryWindowId;

        private Gameplay _gameplay;
        private ContainerBuilder _builder;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _gameplay = new Gameplay(_defeatWindowId, _victoryWindowId);

            _builder.AddSingleton(_gameplay);
            
            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;
            
            _gameplay.Initialize(
                container.Resolve<Level>(),
                container.Resolve<Map>(),
                container.Resolve<CoroutineTimer>(),
                container.Resolve<IWindowService>());
        }
    }
}