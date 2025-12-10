namespace Camera
{
    using Map;
    using MirraGames.SDK;
    using Reflex.Attributes;
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class CameraSizeSetter : MonoBehaviour
    {
        private const float AdditionalSize = 0.5f;
        private const float HorizontalFactor = 0.5f;
        private const float VerticalFactor = 1f;
        
        private Camera _camera;
        private Map _map;

        [Inject]
        private void Initialize(Map map) =>
            _map = map;

        private void Awake() =>
            _camera = GetComponent<Camera>();

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
                _camera.orthographicSize =
                    Mathf.Max(_map.Size.x, _map.Size.y) *
                    (MirraSDK.Device.IsMobile ? VerticalFactor : HorizontalFactor) + AdditionalSize;
            }
        }
    }
}
