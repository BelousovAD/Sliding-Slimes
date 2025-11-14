namespace Bootstrap
{
    using System;
    using System.Collections.Generic;
    using Savvy.Container;
    using Savvy.Interfaces;
    using UnityEngine;

    internal class Loader : MonoSavvy
    {
        [SerializeField] private string _sceneToLoad;
        [SerializeField] private List<MonoBehaviour> _loaders = new();
        
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
            
            GetService<ISceneLoaderService>().LoadAdditiveSceneAsync(_sceneToLoad);
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