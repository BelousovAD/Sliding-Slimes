// namespace Map
// {
//     using Model;
//     using UnityEngine;
//
//     [RequireComponent(typeof(Collider))]
//     public class LuckyBlockDamager : MonoBehaviour
//     {
//         [SerializeField] private LuckyBlockProvider _luckyBlockProvider;
//         
//         private LuckyBlock _luckyBlock;
//
//         private void Awake() =>
//             _luckyBlock = _luckyBlockProvider.Model;
//
//         private void OnCollisionEnter(Collision other)
//         {
//             if (other.gameObject.TryGetComponent(out IModelProvider modelProvider) && modelProvider.Model is Slime)
//             {
//                 _luckyBlock.CountDown();
//             }
//         }
//     }
// }