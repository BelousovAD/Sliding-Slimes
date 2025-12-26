using MirraGames.SDK;
using Reflex.Attributes;
using UnityEngine;

namespace Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraSizeSetter : MonoBehaviour
    {
        private const float AdditionalSize = 0.5f;
        private const float HorizontalFactor = 0.5f;
        private const float VerticalFactor = 1f;

        private UnityEngine.Camera _camera;
        private Map.Map _map;

        [Inject]
        private void Initialize(Map.Map map) =>
            _map = map;

        private void Awake() =>
            _camera = GetComponent<UnityEngine.Camera>();

        private void OnEnable()
        {
            _map.Initialized += SetSize;
            SetSize();
        }

        private void OnDisable() =>
            _map.Initialized -= SetSize;

        private void SetSize()
        {
            if (_map.IsInitialized)
            {
                if (MirraSDK.Device.IsMobile)
                {
                    _camera.orthographicSize = Mathf.Min(_map.Size.x, _map.Size.y) * VerticalFactor - AdditionalSize;
                }
                else
                {
                    _camera.orthographicSize = Mathf.Max(_map.Size.x, _map.Size.y) * HorizontalFactor + AdditionalSize;
                }
            }
        }
    }
}