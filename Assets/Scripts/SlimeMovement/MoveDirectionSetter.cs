using UnityEngine;

namespace SlimeMovement
{
    [RequireComponent(typeof(Collider))]
    internal class MoveDirectionSetter : MonoBehaviour
    {
        private const float CollinearDot = 1f;

        [SerializeField] private bool _invertSlimeDirection;
        [SerializeField] private Vector2Int _direction;

        private Vector2Int _runtimeDirection;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Mover slimeMover) &&
                Mathf.Approximately(
                    Vector3.Dot(slimeMover.Direction, other.contacts[0].normal),
                    CollinearDot))
            {
                _runtimeDirection = _invertSlimeDirection
                    ? Vector2Int.RoundToInt(Quaternion.Euler(90f, 180f, 0f) * slimeMover.Direction)
                    : Vector2Int.RoundToInt(Quaternion.Euler(0f, 0f, -transform.localRotation.eulerAngles.y) *
                                            new Vector3(_direction.x, _direction.y, 0f));

                slimeMover.Move(_runtimeDirection);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Mover slimeMover))
            {
                _runtimeDirection = _invertSlimeDirection
                    ? Vector2Int.RoundToInt(Quaternion.Euler(90f, 180f, 0f) * slimeMover.Direction)
                    : Vector2Int.RoundToInt(Quaternion.Euler(0f, 0f, -transform.localRotation.eulerAngles.y) *
                                            new Vector3(_direction.x, _direction.y, 0f));

                slimeMover.Move(_runtimeDirection);
            }
        }
    }
}