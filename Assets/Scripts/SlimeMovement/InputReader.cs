namespace SlimeMovement
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    internal class InputReader : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        private bool _isCached;
        private Vector2 _startPoint;

        public event Action<Vector2Int> MoveRequested;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isCached = true;
            _startPoint = eventData.position;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isCached)
            {
                _isCached = false;
                Vector2Int direction = Vector2Int.RoundToInt((eventData.position - _startPoint).normalized);

                if (direction.sqrMagnitude > 1)
                {
                    direction.x = 0;
                }
                
                MoveRequested?.Invoke(direction);
            }
        }
    }
}