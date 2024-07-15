using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.Buttons
{
    public class ResumeGameButtonObserver
    {
        private readonly Button button;
        private readonly GameStateManager gameStateManager;

        [Inject]
        public ResumeGameButtonObserver(Button button, GameStateManager gameStateManager)
        {
            this.button = button;
            this.gameStateManager = gameStateManager;

            button.onClick.AddListener(gameStateManager.ResumeGame);
        }

        ~ResumeGameButtonObserver()
        {
            button.onClick.RemoveListener(gameStateManager.ResumeGame);
        }
    }
}