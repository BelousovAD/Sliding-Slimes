namespace Map
{
    using UnityEngine;

    internal class SlimeMoveDirectionResetter : MonoBehaviour
    {
        private const float CollinearDot = 1f;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out SlimeMover slimeMover) &&
                Mathf.Approximately(
                    Vector3.Dot(slimeMover.Direction, other.contacts[0].normal),
                    CollinearDot))
            {
                slimeMover.Move(Vector2Int.zero);
            }
        }
    }
}