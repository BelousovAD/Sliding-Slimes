namespace Ability
{
    using Reflex.Attributes;
    using UnityEngine;

    public class AbilityProviderSpawner : MonoBehaviour
    {
        [SerializeField] private AbilityProvider _prefab;
        [SerializeField] private Transform _parent;

        [Inject]
        private void Initialize(
            Hammer hammer,
            Hourglass hourglass,
            Lightning lightning,
            Megaphone megaphone)
        {
            Instantiate(_prefab, _parent).Initialize(hourglass);
            Instantiate(_prefab, _parent).Initialize(megaphone);
            Instantiate(_prefab, _parent).Initialize(lightning);
            Instantiate(_prefab, _parent).Initialize(hammer);
        }
    }
}