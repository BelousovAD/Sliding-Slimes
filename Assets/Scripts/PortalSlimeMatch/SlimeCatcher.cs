namespace PortalSlimeMatch
{
    using Model;
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    internal class SlimeCatcher : MonoBehaviour
    {
        [SerializeField] private Portal _portal;
        
        private Slime _slime;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_slime is null
                && other.TryGetComponent(out Slime slime)
                && slime.Type == _portal.Type)
            {
                _slime = slime;
                _slime.Catch();
                _portal.CatchSlime();
            }
        }
    }
}