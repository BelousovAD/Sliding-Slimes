namespace Audio
{
    using Bootstrap;
    using Reflex.Core;
    using UnityEngine;
    using UnityEngine.Audio;

    internal class AudioInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private AudioMixer _audioMixer;

        private readonly Music _music = new();
        private readonly Sound _sound = new();
        private ContainerBuilder _builder;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;

            _builder
                .AddSingleton(_music)
                .AddSingleton(_sound)
                .AddSingleton(new AudioMixerController(_audioMixer, _music, _sound));
            
            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;
            
            _music.Initialize(container.Resolve<SavvyServicesProvider>());
            _sound.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}