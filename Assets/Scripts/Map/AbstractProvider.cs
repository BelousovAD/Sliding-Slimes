namespace Map
{
    using UnityEngine;

    internal abstract class AbstractProvider<T> : MonoBehaviour, IModelProvider where T : AbstractModel
    {
        public AbstractModel Model { get; private set; }

        public void Initialize(T model) =>
            Model = model;
    }
}