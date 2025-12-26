using UnityEngine;

namespace Ability
{
    public class AbilityProvider : MonoBehaviour
    {
        public Ability Ability { get; private set; }

        public void Initialize(Ability ability) =>
            Ability = ability;
    }
}