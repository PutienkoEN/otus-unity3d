using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class GameStartTimer : MonoBehaviour
    {
        [SerializeField] private Timer timer = new();
        [SerializeField] private TextMeshProUGUI text;

        private void Awake()
        {
            enabled = false;
        }

        public void StartTimer(Action actionAfterTimerFinish)
        {
            timer.OnTick += UpdateText;
            timer.OnFinish += actionAfterTimerFinish;
            timer.OnFinish += DisableText;
            StartCoroutine(timer.Start());
        }

        private void UpdateText(float time)
        {
            text.text = time.ToString(CultureInfo.InvariantCulture);
        }

        private void DisableText()
        {
            gameObject.SetActive(false);
        }
    }
}