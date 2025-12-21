namespace Level
{
    using Reflex.Attributes;
    using Spawn;

    internal class LevelButtonSpawner : SiblingsSpawner
    {
        private Level _level;

        [Inject]
        private void Initialize(Level level) =>
            _level = level;

        private void Start()
        {
            for (int i = 0; i < _level.Max; i++)
            {
                PooledComponent pooledComponent = Spawn();
                LevelButton button = pooledComponent.GetComponent<LevelButton>();
                button.Initialize(i + 1);
            }
        }
    }
}