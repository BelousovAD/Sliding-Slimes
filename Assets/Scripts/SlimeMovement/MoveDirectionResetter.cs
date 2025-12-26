namespace SlimeMovement
{
    using UnityEngine;

    public class MoveDirectionResetter : MonoBehaviour
    {
        [SerializeField] private Transform _raycastSource;
        [SerializeField] private float _raycastDistance;
        [SerializeField] private LayerMask _targetLayers;
        [SerializeField] private Mover _mover;

        private void FixedUpdate()
        {
            if (_mover.Direction != Vector3.zero
                &&
                Physics.Raycast(
                    _raycastSource.position,
                    _mover.Direction,
                    out RaycastHit hit,
                    _raycastDistance,
                    _targetLayers)
                && gameObject.layer != hit.transform.gameObject.layer)
            {
                _mover.Move(Vector2Int.zero);
            }
        }
    }
}