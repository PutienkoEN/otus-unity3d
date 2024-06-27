using UnityEngine;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStartTimer gameStartTimer;

        private GameListenerManager gameListenerManager = new();

        public void StartGame()
        {
            gameStartTimer.StartTimer(GameStartedAction);
        }

        private void GameStartedAction()
        {
            Debug.Log("Game started!");
        }

        public void FinishGame()
        {
            Debug.Log("Game finished!");
        }

        public void PauseGame()
        {
            gameListenerManager.TriggerGamePauseListeners();
            Debug.Log("Game paused!");
        }

        public void ResumeGame()
        {
            gameListenerManager.TriggerGameResumeListeners();
            Debug.Log("Game resumed!");
        }
    }

    internal enum GameState
    {
        InProgress,
        Finished
    }
}