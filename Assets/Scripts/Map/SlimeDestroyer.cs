namespace Map
{
    using UnityEngine;

    public class SlimeDestroyer : MonoBehaviour
    {
        [SerializeField] private SlimeProvider _slimeProvider;

        private Slime _slime;

        private void Start()
        {
            _slime = _slimeProvider.Model;
            _slime.Caught += Destroy;
        }

        private void OnDestroy() =>
            _slime.Caught -= Destroy;

        private void Destroy() =>
            Destroy(gameObject);
    }
}