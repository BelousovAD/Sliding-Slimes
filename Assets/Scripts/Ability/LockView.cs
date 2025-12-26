using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace Ability
{
    public class LockView : MonoBehaviour
    {
        [SerializeField] private AbilityProvider _abilityProvider;
        [SerializeField] private List<GameObject> _lockObjects = new ();
        [SerializeField] private List<GameObject> _unlockObjects = new ();

        private Ability _ability;
        private Level.Level _level;

        [Inject]
        private void Initialize(Level.Level level) =>
            _level = level;

        private void Start()
        {
            _ability = _abilityProvider.Ability;
            _level.AvailableChanged += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _level.AvailableChanged -= UpdateView;

        private void UpdateView()
        {
            _lockObjects.ForEach(obj => obj.SetActive(_level.Available < _ability.UnlockLevel));
            _unlockObjects.ForEach(obj => obj.SetActive(_level.Available >= _ability.UnlockLevel));
        }
    }
}