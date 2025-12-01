namespace Map
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    internal class SlimeCatcher : MonoBehaviour
    {
        [SerializeField] private PortalProvider _portalProvider;

        private Portal _portal;
        private Slime _slime;
        
        private void OnTriggerEnter(Collider other)
        {
            _portal ??= _portalProvider.Model;

            if (_slime is null
                && other.TryGetComponent(out SlimeProvider slimeProvider)
                && slimeProvider.Model.Type == _portal.Type)
            {
                _slime = slimeProvider.Model;
                slimeProvider.Model.Catch();
                _portal.CatchSlime();
            }
        }
    }
}