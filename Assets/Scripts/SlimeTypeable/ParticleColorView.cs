namespace SlimeTypeable
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(ParticleSystem))]
    internal class ParticleColorView : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _slimeTypeableComponent;

        private ISlimeTypeable _slimeTypeable;
        private ParticleSystem _particleSystem;

        private Color Color
        {
            get
            {
                return _slimeTypeable.Type switch
                {
                    SlimeType.Blue => new Color(0f, 0.5f, 1f),
                    SlimeType.Grey => Color.grey,
                    SlimeType.Orange => new Color(1f, 0.5f, 0f),
                    SlimeType.Purple => Color.magenta,
                    SlimeType.Red => Color.red,
                    SlimeType.White => Color.white,
                    SlimeType.Yellow => Color.yellow,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private void Awake() =>
            _particleSystem = GetComponent<ParticleSystem>();
        
        private void Start()
        {
            _slimeTypeable = _slimeTypeableComponent as ISlimeTypeable ?? throw new InvalidOperationException();
            ParticleSystem.MainModule particleSystemMain = _particleSystem.main;
            particleSystemMain.startColor = Color;
        }

        private void OnValidate()   
        {
            if (_slimeTypeableComponent is not null && _slimeTypeableComponent is not ISlimeTypeable)
            {
                Debug.LogError($"{nameof(_slimeTypeableComponent)} must inherit {nameof(ISlimeTypeable)}");
                _slimeTypeableComponent = null;
            }
        }
    }
}