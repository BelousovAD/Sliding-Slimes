using UnityEngine;

namespace Gameplay
{
    internal class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationPerSecond;

        private void Update() =>
            transform.Rotate(_rotationPerSecond * Time.deltaTime);
    }
}