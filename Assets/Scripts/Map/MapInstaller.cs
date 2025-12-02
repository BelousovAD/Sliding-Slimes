namespace Map
{
    using System.Collections.Generic;
    using Level;
    using Reflex.Core;
    using UnityEngine;

    internal class MapInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MapBuilder _mapBuilder;
        [SerializeField] private List<TextAsset> _maps = new();

        private ContainerBuilder _builder;
        private Map _map;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _map = new Map();

            _builder.AddSingleton(_map);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _mapBuilder.Build(container.Resolve<Map>(), _maps[container.Resolve<Level>().Chosen - 1]);
        }
    }
}
