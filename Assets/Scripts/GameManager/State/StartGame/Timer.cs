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

        public Action<float> Tick;
        public Action Finish;
        private float timeLeft;

        public IEnumerator Start()
        {
            timeLeft = timeInSeconds;

            var delay = new WaitForSeconds(tickInSeconds);
            while (timeLeft > 0)
            {
                Tick?.Invoke(timeLeft);
                timeLeft -= tickInSeconds;
                yield return delay;
            }

            if (timeLeft <= 0)
            {
                Finish?.Invoke();
            }
        }
    }
}