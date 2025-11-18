namespace Level
{
    using System.Collections.Generic;
    using Reflex.Attributes;
    using UnityEngine;

    internal class MaxLevelDependentObjectsSwitcher : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _ordinaryLevelObjects = new();
        [SerializeField] private List<GameObject> _maxLevelObjects = new();
        
        private Level _level;

        [Inject]
        private void Initialize(Level level) =>
            _level = level;

        private void OnEnable()
        {
            _level.ChosenChanged += SwitchObjects;
            SwitchObjects();
        }

        private void OnDisable() =>
            _level.ChosenChanged -= SwitchObjects;

        private void SwitchObjects()
        {
            _ordinaryLevelObjects.ForEach(obj => obj.SetActive(_level.Chosen < _level.Max));
            _maxLevelObjects.ForEach(obj => obj.SetActive(_level.Chosen == _level.Max));
        }
    }
}