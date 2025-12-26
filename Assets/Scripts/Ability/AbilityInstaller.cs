using System.Collections.Generic;
using System.Linq;
using Bootstrap;
using Currency;
using Reflex.Core;
using Timer;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Ability
{
    internal class AbilityInstaller : MonoBehaviour, IInstaller
    {
        private const AudioType SoundType = AudioType.Sound;

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

            Audio.Audio sound = container.Resolve<IEnumerable<Audio.Audio>>()
                .FirstOrDefault(audioObject => audioObject.Type == SoundType);

            _hammer.Initialize(
                container.Resolve<SavvyServicesProvider>(),
                container.Resolve<Money>(),
                container.Resolve<Map.Map>());
            _hourglass.Initialize(
                container.Resolve<SavvyServicesProvider>(),
                sound,
                container.Resolve<Money>(),
                container.Resolve<CoroutineTimer>());
            _lightning.Initialize(
                container.Resolve<SavvyServicesProvider>(),
                container.Resolve<Money>(),
                container.Resolve<Map.Map>());
            _megaphone.Initialize(
                container.Resolve<SavvyServicesProvider>(),
                sound,
                container.Resolve<Money>(),
                container.Resolve<Map.Map>());
        }
    }
}