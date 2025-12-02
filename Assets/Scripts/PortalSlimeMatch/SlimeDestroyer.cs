namespace PortalSlimeMatch
{
    using Model;
    using UnityEngine;

    internal class SlimeDestroyer : MonoBehaviour
    {
        [SerializeField] private Slime _slime;

        private void OnEnable() =>
            _slime.Caught += Destroy;

        private void OnDisable() =>
            _slime.Caught -= Destroy;

        private void Destroy() =>
            Destroy(gameObject);
    }
}