namespace Map
{
    using System.Collections.Generic;
    using UnityEngine;

    internal class SlimeSleepView : MonoBehaviour
    {
        [SerializeField] private SlimeProvider _slimeProvider;
        [SerializeField] private List<GameObject> _awakeObjects = new();
        [SerializeField] private List<GameObject> _sleepObjects = new();

        private Slime _slime;

        private void OnEnable() =>
            UpdateView();

        private void Start()
        {
            _slime = _slimeProvider.Model;
            _slime.CountChanged += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _slime.CountChanged -= UpdateView;

        private void UpdateView()
        {
            if (_slime is null)
            {
                return;
            }
            
            _awakeObjects.ForEach(obj => obj.SetActive(_slime.IsSleeping == false));
            _sleepObjects.ForEach(obj => obj.SetActive(_slime.IsSleeping));
        }
    }
}