namespace Bootstrap
{
    using Savvy.Bootstrap;
    using Savvy.Interfaces;
    using UnityEngine;

    public class ProjectBehaviour : ProjectBehaviourBase
    {
        [SerializeField] private string _sceneToLoad;
        
        protected override void PostAwake() =>
            GetService<ISceneLoaderService>().LoadAdditiveSceneAsync(_sceneToLoad);
    }
}
