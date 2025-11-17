namespace Bootstrap
{
    using Reflex.Core;
    using UnityEngine;

    public class BootstrapInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new SavvyServicesProvider());
    }
}