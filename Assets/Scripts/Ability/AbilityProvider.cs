namespace Ability
{
    using UnityEngine;

    public class AbilityProvider : MonoBehaviour
    {
        public Ability Ability { get; private set; }

        public void Initialize(Ability ability) =>
            Ability = ability;
    }
}