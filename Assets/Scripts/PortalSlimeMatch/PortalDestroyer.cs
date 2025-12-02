namespace PortalSlimeMatch
{
    using Model;
    using UnityEngine;

    internal class PortalDestroyer : MonoBehaviour
    {
        [SerializeField] private Portal _portal;

        private void OnEnable() =>
            _portal.SlimeCaught += Destroy;

        private void OnDisable() =>
            _portal.SlimeCaught -= Destroy;

        private void Destroy() =>
            Destroy(gameObject);
    }
}