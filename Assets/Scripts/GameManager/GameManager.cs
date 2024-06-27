using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStartTimer gameStartTimer;

        private GameState currentState = GameState.None;
        private GameListenerManager gameListenerManager = new();

        private State StartGameState = new(new HashSet<GameState> { GameState.None });
        private State PauseGameState = new(new HashSet<GameState> { GameState.InProgress });
        private State ResumeGameState = new(new HashSet<GameState> { GameState.Paused });
        private State FinishGameState = new(new HashSet<GameState> { GameState.InProgress });

        public void StartGame()
        {
            if (StartGameState.IsNotAllowedTransition(currentState))
            {
                return;
            }

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
            if (FinishGameState.IsNotAllowedTransition(currentState))
            {
                return;
            }

            gameListenerManager.TriggerGameFinishListeners();
            currentState = GameState.Finished;

            Debug.Log("Game finished!");
        }

        public void PauseGame()
        {
            if (PauseGameState.IsNotAllowedTransition(currentState))
            {
                return;
            }

            gameListenerManager.TriggerGamePauseListeners();
            currentState = GameState.Paused;

            Debug.Log("Game paused!");
        }

        public void ResumeGame()
        {
            if (ResumeGameState.IsNotAllowedTransition(currentState))
            {
                return;
            }

            gameListenerManager.TriggerGameResumeListeners();
            currentState = GameState.InProgress;

            Debug.Log("Game resumed!");
        }
    }

    public enum GameState
    {
        None,
        AwaitStart,
        InProgress,
        Paused,
        Finished
    }

    public class State
    {
        private readonly HashSet<GameState> allowedStates;

        public State(HashSet<GameState> allowedStates)
        {
            this.allowedStates = allowedStates;
        }

        public bool IsAllowedTransition(GameState gameState)
        {
            return allowedStates.Contains(gameState);
        }

        public bool IsNotAllowedTransition(GameState gameState)
        {
            return !IsAllowedTransition(gameState);
        }
    }
}