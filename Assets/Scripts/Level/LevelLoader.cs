using Bootstrap;
using Reflex.Attributes;
using UnityEngine;

namespace Level
{
    internal class LevelLoader : MonoBehaviour, ILoadable
    {
        private Level _level;

        [Inject]
        private void Initialize(Level level) =>
            _level = level;

        public void Load() =>
            _level.Load();
    }
}