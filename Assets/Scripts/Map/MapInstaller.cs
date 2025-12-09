namespace Map
{
    using Reflex.Core;
    using UnityEngine;

    internal class MapInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new Map());
    }
}