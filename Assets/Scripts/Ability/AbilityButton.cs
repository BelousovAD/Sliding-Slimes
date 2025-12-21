namespace Ability
{
    using Common;
    using UnityEngine;

    public class AbilityButton : AbstractButton
    {
        [SerializeField] private AbilityProvider _abilityProvider;

        protected override void HandleClick() =>
            _abilityProvider.Ability.Use();
    }
}