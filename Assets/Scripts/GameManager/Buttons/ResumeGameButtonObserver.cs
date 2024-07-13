using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.Buttons
{
    public class ResumeGameButtonObserver
    {
        private readonly Button button;
        private readonly GameManager gameManager;

        [Inject]
        public ResumeGameButtonObserver(Button button, GameManager gameManager)
        {
            this.button = button;
            this.gameManager = gameManager;

            button.onClick.AddListener(gameManager.ResumeGame);
        }

        ~ResumeGameButtonObserver()
        {
            button.onClick.RemoveListener(gameManager.ResumeGame);
        }
    }
}