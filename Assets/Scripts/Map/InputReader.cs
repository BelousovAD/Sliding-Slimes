namespace Map
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class InputReader : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        private const float CollinearEpsilon = 0.5f;
        
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
            Vector2 direction = eventData.position - _startPoint;

            if (Vector2.Dot(Vector2.up, direction) > CollinearEpsilon)
            {
                MoveRequested?.Invoke(Vector2Int.up);
            }
            else if (Vector2.Dot(Vector2.right, direction) > CollinearEpsilon)
            {
                MoveRequested?.Invoke(Vector2Int.right);
            }
            else if (Vector2.Dot(Vector2.down, direction) > CollinearEpsilon)
            {
                MoveRequested?.Invoke(Vector2Int.down);
            }
            else
            {
                MoveRequested?.Invoke(Vector2Int.left);
            }
        }
    }
}