namespace Timer
{
    using System;
    using System.Collections;
    using Bootstrap;
    using UnityEngine;

    internal class CoroutineTimer
    {
        private readonly WaitForSeconds _delay = new(1);
        private Coroutine _coroutine;
        private SavvyServicesProvider _services;
        private int _time;

        public CoroutineTimer(int max) =>
            Max = max;

        public event Action TimeChanged;
        public event Action TimeIsUp;
        
        public int Max { get; }

        public int Time
        {
            get
            {
                return _time;
            }

            private set
            {
                if (value != _time)
                {
                    _time = value;
                    TimeChanged?.Invoke();
                }
            }
        }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public bool TryAdd(int seconds)
        {
            if (Time + seconds > Max)
            {
                return false;
            }
            
            Time += seconds;
            
            _services.CoroutineRunner.StopCoroutine(_coroutine);
            _coroutine = _services.CoroutineRunner.StartCoroutine(Countdown());

            return true;
        }

        private IEnumerator Countdown()
        {
            while (Time > 0)
            {
                yield return _delay;

                Time--;
            }
            
            TimeIsUp?.Invoke();
        }
    }
}