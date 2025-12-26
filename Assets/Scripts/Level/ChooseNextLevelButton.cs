using Common;
using Reflex.Attributes;

namespace Level
{
    internal class ChooseNextLevelButton : AbstractButton
    {
        private Level _level;

        [Inject]
        private void Initialize(Level level) =>
            _level = level;

        protected override void HandleClick() =>
            _level.Choose(_level.Chosen + 1);
    }
}