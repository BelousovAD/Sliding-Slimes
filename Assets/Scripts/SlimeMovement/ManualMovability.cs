namespace SlimeMovement
{
    using System;
    using Map;
    using UnityEngine;

    internal class ManualMovability : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _slimeProvider;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Mover _mover;

        private Slime _slime;
        
        private void Start() =>
            _slime = ((IModelProvider)_slimeProvider).Model as Slime
                     ?? throw new InvalidOperationException();

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

        private void OnValidate()
        {
            if (_slimeProvider is not (null or IModelProvider))
            {
                Debug.LogError($"{nameof(_slimeProvider)} must inherit {nameof(IModelProvider)}");
                _slimeProvider = null;
            }
        }
    }
}