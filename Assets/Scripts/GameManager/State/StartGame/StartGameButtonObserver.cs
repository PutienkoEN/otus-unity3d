using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.Buttons
{
    public class StartGameButtonObserver 
    {
        private readonly Button button;
        private readonly GameStateManager gameStateManager;

        [Inject]
        public StartGameButtonObserver(Button button, GameStateManager gameStateManager)
        {
            this.button = button;
            this.gameStateManager = gameStateManager;

            button.onClick.AddListener(gameStateManager.StartGame);
        }

        ~StartGameButtonObserver()
        {
            button.onClick.RemoveListener(gameStateManager.StartGame);
        }
    }
}