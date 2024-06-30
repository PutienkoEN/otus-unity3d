using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStartTimer gameStartTimer;

        private GameState currentState = GameState.None;
        private readonly GameListenerManager gameListenerManager = new();

        private readonly State startGameState = new(new HashSet<GameState> { GameState.None });
        private readonly State pauseGameState = new(new HashSet<GameState> { GameState.InProgress });
        private readonly State resumeGameState = new(new HashSet<GameState> { GameState.Paused });
        private readonly State finishGameState = new(new HashSet<GameState> { GameState.InProgress });

        private void Update()
        {
            if (currentState != GameState.InProgress)
            {
                return;
            }

            gameListenerManager.TriggerGameUpdateListeners();
        }

        private void FixedUpdate()
        {
            if (currentState != GameState.InProgress)
            {
                return;
            }

            gameListenerManager.TriggerGameFixedUpdateListeners();
        }

        public void StartGame()
        {
            if (startGameState.IsNotAllowedTransition(currentState))
            {
                return;
            }

            // Prevents start to be clicked twice while timer is running.
            currentState = GameState.AwaitStart;
            gameStartTimer.StartTimer(GameStartedAction);
        }

        private void GameStartedAction()
        {
            gameListenerManager.TriggerGameStartListeners();
            currentState = GameState.InProgress;

            Debug.Log("Game started!");
        }

        public void FinishGame()
        {
            if (finishGameState.IsNotAllowedTransition(currentState))
            {
                return;
            }

            gameListenerManager.TriggerGameFinishListeners();
            currentState = GameState.Finished;

            Debug.Log("Game finished!");
        }

        public void PauseGame()
        {
            if (pauseGameState.IsNotAllowedTransition(currentState))
            {
                return;
            }

            gameListenerManager.TriggerGamePauseListeners();
            currentState = GameState.Paused;

            Debug.Log("Game paused!");
        }

        public void ResumeGame()
        {
            if (resumeGameState.IsNotAllowedTransition(currentState))
            {
                return;
            }

            gameListenerManager.TriggerGameResumeListeners();
            currentState = GameState.InProgress;

            Debug.Log("Game resumed!");
        }
    }
}