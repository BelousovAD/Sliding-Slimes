namespace SceneManagement
{
    using Bootstrap;
    using Common;
    using Reflex.Attributes;
    using UnityEngine;

    internal class LoadSceneButton : AbstractButton
    {
        [SerializeField] private string _sceneToLoad;
        
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        protected override void HandleClick() =>
            _services.SceneLoader.LoadAdditiveSceneAsync(_sceneToLoad);
    }
}