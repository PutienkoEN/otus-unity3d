using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.Buttons
{
    /**
     * Only one controller for several buttons for simplicity.
     */
    public class ButtonController
    {
        private readonly LevelProvider levelProvider;
        private readonly GameStateManager gameStateManager;

        [Inject]
        public ButtonController(LevelProvider levelProvider, GameStateManager gameStateManager)
        {
            this.levelProvider = levelProvider;
            this.gameStateManager = gameStateManager;

            AddListenerToButton(levelProvider.startButton, gameStateManager.StartGame);
            AddListenerToButton(levelProvider.pauseButton, gameStateManager.PauseGame);
            AddListenerToButton(levelProvider.resumeButton, gameStateManager.ResumeGame);
        }

        private static void AddListenerToButton(Button button, UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        ~ButtonController()
        {
            RemoveListenerFromButton(levelProvider.startButton, gameStateManager.StartGame);
            RemoveListenerFromButton(levelProvider.pauseButton, gameStateManager.PauseGame);
            RemoveListenerFromButton(levelProvider.resumeButton, gameStateManager.ResumeGame);
        }

        private static void RemoveListenerFromButton(Button button, UnityAction action)
        {
            button.onClick.RemoveListener(action);
        }
    }
}