using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.Buttons
{
    public class StartGameButtonObserver 
    {
        private readonly Button button;
        private readonly GameManager gameManager;

        [Inject]
        public StartGameButtonObserver(Button button, GameManager gameManager)
        {
            this.button = button;
            this.gameManager = gameManager;

            button.onClick.AddListener(gameManager.StartGame);
        }

        ~StartGameButtonObserver()
        {
            button.onClick.RemoveListener(gameManager.StartGame);
        }
    }
}