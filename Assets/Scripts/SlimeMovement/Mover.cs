namespace SlimeMovement
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    internal class Mover : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _speed;
        
        private Rigidbody _rigidbody;
        
        public Vector3 Direction { get; private set; }

        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody>();

        public void Move(Vector2Int direction)
        {
            Direction = new Vector3(direction.x, 0, direction.y);
            _rigidbody.isKinematic = false;
            transform.localPosition = Vector3Int.RoundToInt(transform.localPosition);
            _rigidbody.velocity = Direction * _speed;
            
            if (direction == Vector2Int.zero)
            {
                _rigidbody.isKinematic = true;
            }
        }
    }
}