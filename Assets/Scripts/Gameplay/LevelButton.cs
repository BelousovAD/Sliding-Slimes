namespace Gameplay
{
    using System;
    using Common;
    using Reflex.Attributes;

    internal class LevelButton : AbstractButton
    {
        private Level _level;
        private int _number;

        public event Action NumberChanged;

        public int Number
        {
            get
            {
                return _number;
            }

            private set
            {
                if (value != _number)
                {
                    _number = value;
                    NumberChanged?.Invoke();
                }
            }
        }
        
        [Inject]
        private void Initialize(Level level) =>
            _level = level;

        public void Initialize(int number) =>
            Number = number;

        protected override void OnEnable()
        {
            base.OnEnable();
            _level.AvailableChanged += UpdateInteractable;
            NumberChanged += UpdateInteractable;
            UpdateInteractable();
        }

        protected override void OnDisable()
        {
            NumberChanged -= UpdateInteractable;
            _level.AvailableChanged -= UpdateInteractable;
            base.OnDisable();
        }

        protected override void HandleClick() =>
            _level.Choose(Number);

        private void UpdateInteractable() =>
            Button.interactable = Number <= _level.Available;
    }
}