namespace Window
{
    using Reflex.Core;
    using UnityEngine;

    internal class WindowServiceInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private string _startWindowId;
        [SerializeField] private MonoBehaviour _spawner;

        private ContainerBuilder _builder;
        private WindowService _windowService;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _windowService = new WindowService(_startWindowId, _spawner as IWindowSpawner);
            
            _builder.AddSingleton(_windowService, typeof(IWindowService));
            
            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;
            
            _windowService.Initialize();
        }

        private void OnValidate()
        {
            if (_spawner is not null && _spawner is not IWindowSpawner)
            {
                Debug.LogError($"{nameof(_spawner)} must inherited {nameof(IWindowSpawner)}");
                _spawner = null;
            }
        }
    }
}