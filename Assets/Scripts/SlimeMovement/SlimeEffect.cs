namespace SlimeMovement
{
    using Audio;
    using DG.Tweening;
    using Reflex.Attributes;
    using UnityEngine;

    internal class SlimeEffect : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Transform _model;
        [SerializeField] private Vector3 _scalePunch;
        [SerializeField, Min(0.001f)] private float _animationDuration = 0.001f;

        private Tweener _tweener;
        private Audio _audio;

        [Inject]
        private void Initialize(Sound sound) =>
            _audio = sound;

        private void OnEnable() =>
            _inputReader.MoveRequested += PlayAnimation;

        private void OnDisable()
        {
            _inputReader.MoveRequested -= PlayAnimation;
            _tweener.Kill(true);
        }

        private void PlayAnimation(Vector2Int direction)
        {
            if (_tweener.IsActive())
            {
                _tweener.Kill(true);
            }
            
            _audio.Play(AudioClipKey.Slime);
            _tweener = _model.DOPunchScale(_scalePunch, _animationDuration);
        }
    }
}