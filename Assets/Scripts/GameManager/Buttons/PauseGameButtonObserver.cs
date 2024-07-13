using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.Buttons
{
    public class PauseGameButtonObserver 
    {
        private readonly Button button;
        private readonly GameManager gameManager;

        [Inject]
        public PauseGameButtonObserver(Button button, GameManager gameManager) 
        {
            this.button = button;
            this.gameManager = gameManager;
            
            button.onClick.AddListener(gameManager.PauseGame);
        }

        ~PauseGameButtonObserver()
        {
            button.onClick.RemoveListener(gameManager.PauseGame);
        }
    }
}