namespace SlimeMovement
{
    using Model;
    using UnityEngine;

    internal class ManualMovability : MonoBehaviour
    {
        [SerializeField] private Slime _slime;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Mover _mover;

        private void OnEnable() =>
            _inputReader.MoveRequested += Move;

        private void OnDisable() =>
            _inputReader.MoveRequested -= Move;

        private void Move(Vector2Int direction)
        {
            if (_slime is not null
                && _mover.Direction == Vector3.zero
                && _slime.IsSleeping == false)
            {
                _mover.Move(direction);
            }
        }
    }
}