namespace Bootstrap
{
    using System;
    using System.Collections.Generic;
    using Reflex.Attributes;
    using Savvy.Container;
    using UnityEngine;

    internal class Loader : MonoSavvy
    {
        [SerializeField] private string _sceneToLoad;
        [SerializeField] private List<MonoBehaviour> _loaders = new();

        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        private void Start()
        {
            foreach (MonoBehaviour monoBehaviour in _loaders)
            {
                if (monoBehaviour is ILoadable loader)
                {
                    loader.Load();
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Unexpected loader that is null or does not inherit {nameof(ILoadable)}");
                }
            }
            
            _services.SceneLoader.LoadAdditiveSceneAsync(_sceneToLoad);
        }

        private void OnValidate()
        {
            foreach (MonoBehaviour monoBehaviour in _loaders)
            {
                if (monoBehaviour is not null && monoBehaviour is not ILoadable)
                {
                    Error($"Elements in {nameof(_loaders)} must inherit {nameof(ILoadable)}");
                }
            }
        }
    }
}