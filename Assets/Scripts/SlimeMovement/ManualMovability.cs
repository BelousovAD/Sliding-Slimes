namespace SlimeMovement
{
    using SlimeSleep;
    using UnityEngine;

    internal class ManualMovability : MonoBehaviour
    {
        [SerializeField] private SleepCounter _sleepCounter;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Mover _mover;

        private void OnEnable() =>
            _inputReader.MoveRequested += Move;

        private void OnDisable() =>
            _inputReader.MoveRequested -= Move;

        private void Move(Vector2Int direction)
        {
            if (_sleepCounter is not null
                && _mover.Direction == Vector3.zero
                && _sleepCounter.IsSleepActive == false)
            {
                _mover.Move(direction);
            }
        }
    }
}