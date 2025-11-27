namespace Map
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    internal class PortalBreaker : MonoBehaviour
    {
        [SerializeField] private PortalProvider _portalProvider;

        private Portal _portal;
        
        private void OnTriggerEnter(Collider other)
        {
            _portal ??= _portalProvider.Model;

            if (other.TryGetComponent(out SlimeProvider slimeProvider)
                && slimeProvider.Model.Type == _portal.Type)
            {
                _portal.Disappear(slimeProvider.Model);
            }
        }
    }
}