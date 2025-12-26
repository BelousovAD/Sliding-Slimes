using Common;
using UnityEngine;

namespace Ability
{
    public class AbilityButton : AbstractButton
    {
        [SerializeField] private AbilityProvider _abilityProvider;

        protected override void HandleClick() =>
            _abilityProvider.Ability.Use();
    }
}