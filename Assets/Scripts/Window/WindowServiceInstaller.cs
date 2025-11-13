namespace Window
{
    using Reflex.Core;
    using UnityEngine;

    internal class WindowServiceInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private string _startWindowId;
        [SerializeField] private MonoBehaviour _spawner;
        
        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new WindowService(_startWindowId, _spawner as IWindowSpawner), typeof(IWindowService));

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