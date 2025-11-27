namespace Map
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class SlimeMover : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField, Min(0f)] private float _speed;
        
        private Rigidbody _rigidbody;
        
        public Vector3 Direction { get; private set; }

        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody>();

        private void OnEnable() =>
            _inputReader.MoveRequested += Move;

        private void OnDisable() =>
            _inputReader.MoveRequested -= Move;

        public void Move(Vector2Int direction)
        {
            Direction = new Vector3(direction.x, 0, direction.y);
            _rigidbody.isKinematic = false;
            
            if (direction == Vector2Int.zero)
            {
                _rigidbody.velocity = Vector3.zero;
                transform.localPosition = Vector3Int.RoundToInt(transform.localPosition);
                _rigidbody.isKinematic = true;
            }
            else
            {
                _rigidbody.velocity = Direction * _speed;
            }
        }
    }
}