namespace Travelator
{
    using Model;
    using UnityEngine;

    internal class DirectionView : MonoBehaviour
    {
        [SerializeField] private Travelator _travelator;

        private Vector2Int _direction;

        private void OnEnable()
        {
            _travelator.TypeChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _travelator.TypeChanged -= UpdateView;

        private void UpdateView()
        {
            _direction = _travelator.Direction;
            transform.forward = new Vector3(_direction.x, 0f, _direction.y);
        }
    }
}