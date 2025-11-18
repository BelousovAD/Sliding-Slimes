namespace Gameplay
{
    using Reflex.Attributes;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    internal class SkipGameButton : MonoBehaviour
    {
        [SerializeField] private bool _win;
        
        private Button _button;
        private Gameplay _gameplay;

        [Inject]
        private void Initialize(Gameplay gameplay) =>
            _gameplay = gameplay;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(HandleClick);

        private void OnDisable() =>
            _button.onClick.RemoveListener(HandleClick);

        private void HandleClick()
        {
            if (_win)
            {
                _gameplay.HandleVictory();
            }
            else
            {
                _gameplay.OpenDefeatWindow();
            }
        }
    }
}