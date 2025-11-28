namespace Map
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(MeshRenderer))]
    internal class SlimeTypeColorView : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _slimeTypeableProvider;

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
            _slimeTypeable = ((IModelProvider)_slimeTypeableProvider).Model as ISlimeTypeable
                             ?? throw new InvalidOperationException();
            _meshRenderer.material.color = Color;
        }

        private void OnValidate()
        {
            if (_slimeTypeableProvider is null or IModelProvider)
            {
                return;
            }

            Debug.LogError($"{nameof(_slimeTypeableProvider)} must inherit {nameof(IModelProvider)}");
            _slimeTypeableProvider = null;
        }
    }
}