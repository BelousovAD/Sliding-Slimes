namespace SlimeMovement
{
    using DG.Tweening;
    using UnityEngine;

    public class Animator : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Transform _model;
        [SerializeField] private Vector3 _scalePunch;
        [SerializeField, Min(0.001f)] private float _animationDuration = 0.001f;

        private Tweener _tweener;

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
            
            _tweener = _model.DOPunchScale(_scalePunch, _animationDuration);
        }
    }
}