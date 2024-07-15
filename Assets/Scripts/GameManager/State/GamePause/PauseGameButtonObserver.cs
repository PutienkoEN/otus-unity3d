using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.Buttons
{
    public class PauseGameButtonObserver
    {
        private readonly Button button;
        private readonly GameStateManager gameStateManager;

        [Inject]
        public PauseGameButtonObserver(Button button, GameStateManager gameStateManager)
        {
            this.button = button;
            this.gameStateManager = gameStateManager;

            button.onClick.AddListener(gameStateManager.PauseGame);
        }

        ~PauseGameButtonObserver()
        {
            button.onClick.RemoveListener(gameStateManager.PauseGame);
        }
    }
}