namespace SlimeSleep
{
    using System.Collections.Generic;
    using Model;
    using UnityEngine;

    internal class SlimeSleepView : MonoBehaviour
    {
        [SerializeField] private Slime _slime;
        [SerializeField] private List<GameObject> _awakeObjects = new();
        [SerializeField] private List<GameObject> _sleepObjects = new();

        private void OnEnable()
        {
            _slime.CountChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _slime.CountChanged -= UpdateView;

        private void UpdateView()
        {
            if (_slime is not null)
            {
                _awakeObjects.ForEach(obj => obj.SetActive(_slime.IsSleeping == false));
                _sleepObjects.ForEach(obj => obj.SetActive(_slime.IsSleeping));
            }
        }
    }
}