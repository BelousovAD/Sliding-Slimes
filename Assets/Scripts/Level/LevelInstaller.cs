using Bootstrap;
using Reflex.Core;
using UnityEngine;

namespace Level
{
    internal class LevelInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField][Min(1)] private int _maxLevel = 1;

        private Level _level;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _level = new Level(_maxLevel);

            _builder.AddSingleton(_level);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _level.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}