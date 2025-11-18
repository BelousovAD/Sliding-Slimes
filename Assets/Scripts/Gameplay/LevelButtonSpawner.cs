namespace Gameplay
{
    using Reflex.Attributes;
    using UnityEngine;

    internal class LevelButtonSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private LevelButton _prefab;

        private Level _level;

        [Inject]
        private void Initialize(Level level) =>
            _level = level;

        private void Start()
        {
            for (int i = 0; i < _level.Max; i++)
            {
                LevelButton button = Instantiate(_prefab, _parent);
                button.Initialize(i + 1);
            }
        }
    }
}