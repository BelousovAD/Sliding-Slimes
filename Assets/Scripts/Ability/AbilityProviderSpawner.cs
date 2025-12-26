namespace Ability
{
    using System.Collections.Generic;
    using Reflex.Attributes;
    using Spawn;

    public class AbilityProviderSpawner : SiblingsSpawner
    {
        private readonly List<Ability> _abilities = new ();
        
        [Inject]
        private void Initialize(
            Hammer hammer,
            Hourglass hourglass,
            Lightning lightning,
            Megaphone megaphone)
        {
            _abilities.Add(hourglass);
            _abilities.Add(megaphone);
            _abilities.Add(lightning);
            _abilities.Add(hammer);
        }

        private void Start() =>
            _abilities.ForEach(Spawn);

        private void Spawn(Ability ability) =>
            Spawn().GetComponent<AbilityProvider>().Initialize(ability);
    }
}