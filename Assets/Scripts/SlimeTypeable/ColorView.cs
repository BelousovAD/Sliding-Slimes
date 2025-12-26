namespace SlimeTypeable
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(MeshRenderer))]
    internal class ColorView : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _slimeTypeableComponent;

        private ISlimeTypeable _slimeTypeable;
        private MeshRenderer _meshRenderer;

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
            _meshRenderer = GetComponent<MeshRenderer>();
        
        private void Start()
        {
            _slimeTypeable = _slimeTypeableComponent as ISlimeTypeable ?? throw new InvalidOperationException();
            _meshRenderer.material.color = Color;
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