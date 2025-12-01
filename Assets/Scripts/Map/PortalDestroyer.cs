namespace Map
{
    using UnityEngine;

    public class PortalDestroyer : MonoBehaviour
    {
        [SerializeField] private PortalProvider _portalProvider;

        private Portal _portal;

        private void Start()
        {
            _portal = _portalProvider.Model;
            _portal.SlimeCaught += Destroy;
        }

        private void OnDestroy() =>
            _portal.SlimeCaught -= Destroy;

        private void Destroy() =>
            Destroy(gameObject);
    }
}