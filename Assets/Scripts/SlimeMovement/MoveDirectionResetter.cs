namespace SlimeMovement
{
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    internal class MoveDirectionResetter : MonoBehaviour
    {
        private const float CollinearDot = 1f;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Mover slimeMover) &&
                Mathf.Approximately(
                    Vector3.Dot(slimeMover.Direction, other.contacts[0].normal),
                    CollinearDot))
            {
                slimeMover.Move(Vector2Int.zero);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Mover slimeMover))
            {
                slimeMover.Move(Vector2Int.zero);
            }
        }
    }
}