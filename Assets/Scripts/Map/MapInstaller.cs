using Reflex.Core;
using UnityEngine;

namespace Map
{
    internal class MapInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new Map());
    }
}