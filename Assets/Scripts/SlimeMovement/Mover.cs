using UnityEngine;

namespace SlimeMovement
{
    [RequireComponent(typeof(Rigidbody))]
    internal class Mover : MonoBehaviour
    {
        private static Mover _busyInstance;

        [SerializeField][Min(0f)] private float _speed;

        private Rigidbody _rigidbody;

        public Vector3 Direction { get; private set; }

        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody>();

        public void Move(Vector2Int direction)
        {
            if (_busyInstance is not null && _busyInstance != this)
            {
                return;
            }

            Direction = new Vector3(direction.x, 0, direction.y);
            _rigidbody.isKinematic = false;
            transform.localPosition = Vector3Int.RoundToInt(transform.localPosition);
            _rigidbody.velocity = Direction * _speed;
            _busyInstance = this;

            if (direction == Vector2Int.zero)
            {
                _rigidbody.isKinematic = true;
                _busyInstance = null;
            }
        }
    }
}