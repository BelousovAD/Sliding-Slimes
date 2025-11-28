namespace Map
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    internal class SlimeCatcher : MonoBehaviour
    {
        [SerializeField] private PortalProvider _portalProvider;

        private Portal _portal;
        
        private void OnTriggerEnter(Collider other)
        {
            _portal ??= _portalProvider.Model;

            if (other.TryGetComponent(out SlimeProvider slimeProvider)
                && slimeProvider.Model.Type == _portal.Type)
            {
                slimeProvider.Model.Catch();
                _portal.CatchSlime();
            }
        }
    }
}