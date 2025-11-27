namespace Map
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
            if (_isCached == false)
            {
                return;
            }
            
            _isCached = false;
            Vector2 direction = (eventData.position - _startPoint).normalized;
            MoveRequested?.Invoke(Vector2Int.RoundToInt(direction));
        }
    }
}