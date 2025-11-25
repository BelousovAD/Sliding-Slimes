namespace Map
{
    using UnityEngine;

    internal abstract class AbstractView<T> : MonoBehaviour where T : AbstractModel
    {
        public abstract void Initialize(T model);
    }
}