using System.Collections.Generic;
using UnityEngine;

namespace SlimeSleep
{
    internal class SleepView : MonoBehaviour
    {
        [SerializeField] private SleepCounter _sleepCounter;
        [SerializeField] private List<GameObject> _awakeObjects = new ();
        [SerializeField] private List<GameObject> _sleepObjects = new ();

        private void OnEnable()
        {
            _sleepCounter.CountChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _sleepCounter.CountChanged -= UpdateView;

        private void UpdateView()
        {
            _awakeObjects.ForEach(obj => obj.SetActive(_sleepCounter.IsSleepActive == false));
            _sleepObjects.ForEach(obj => obj.SetActive(_sleepCounter.IsSleepActive));
        }
    }
}