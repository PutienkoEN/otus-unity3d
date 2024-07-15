using System;
using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class Timer
    {
        [SerializeField] private float timeInSeconds = 3;
        [SerializeField] private float tickInSeconds = 1;

        public Action<float> OnTick;
        public Action OnFinish;
        private float timeLeft;

        public IEnumerator Start()
        {
            timeLeft = timeInSeconds;

            var delay = new WaitForSeconds(tickInSeconds);
            while (timeLeft > 0)
            {
                OnTick?.Invoke(timeLeft);
                timeLeft -= tickInSeconds;
                yield return delay;
            }

            if (timeLeft <= 0)
            {
                OnFinish?.Invoke();
            }
        }
    }
}