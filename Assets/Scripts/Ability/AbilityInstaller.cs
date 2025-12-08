namespace Ability
{
    using Bootstrap;
    using Reflex.Core;
    using UnityEngine;

    internal class AbilityInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private AbilityData _hammerData; 
        [SerializeField] private AbilityData _hourglassData; 
        [SerializeField] private AbilityData _lightningData; 
        [SerializeField] private AbilityData _megaphoneData; 
        
        private Hammer _hammer;
        private Hourglass _hourglass;
        private Lightning _lightning;
        private Megaphone _megaphone;
        private ContainerBuilder _builder;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _hammer = new Hammer(_hammerData);
            _hourglass = new Hourglass(_hourglassData);
            _lightning = new Lightning(_lightningData);
            _megaphone = new Megaphone(_megaphoneData);

            _builder.AddSingleton(_hammer);
            _builder.AddSingleton(_hourglass);
            _builder.AddSingleton(_lightning);
            _builder.AddSingleton(_megaphone);
            
            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;
            
            _hammer.Initialize(container.Resolve<SavvyServicesProvider>());
            _hourglass.Initialize(container.Resolve<SavvyServicesProvider>());
            _lightning.Initialize(container.Resolve<SavvyServicesProvider>());
            _megaphone.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}