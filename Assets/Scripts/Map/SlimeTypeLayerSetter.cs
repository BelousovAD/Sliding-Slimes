namespace Map
{
    using System;
    using UnityEngine;

    internal class SlimeTypeLayerSetter : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _slimeTypeableProvider;

        private ISlimeTypeable _slimeTypeable;
        
        private void Start()
        {
            _slimeTypeable = ((IModelProvider)_slimeTypeableProvider).Model as ISlimeTypeable
                             ?? throw new InvalidOperationException();
            gameObject.layer = LayerMask.NameToLayer(Enum.GetName(typeof(SlimeType), _slimeTypeable.Type));
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