using System;
using UnityEngine;

namespace SlimeTypeable
{
    internal class LayerSetter : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _slimeTypeableComponent;

        private ISlimeTypeable _slimeTypeable;

        private void Start()
        {
            _slimeTypeable = _slimeTypeableComponent as ISlimeTypeable ?? throw new InvalidOperationException();
            gameObject.layer = LayerMask.NameToLayer(Enum.GetName(typeof(SlimeType), _slimeTypeable.Type));
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