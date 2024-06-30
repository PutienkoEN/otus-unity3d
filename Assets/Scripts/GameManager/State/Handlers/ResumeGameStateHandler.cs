using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class ResumeGameStateHandler : IGameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly List<IGamePauseListener> gameStartListeners = new();

        public ResumeGameStateHandler(GameStateStorage gameStateStorage)
        {
            this.gameStateStorage = gameStateStorage;
            IGameListener.OnRegister += AddListener;
        }

        public bool IsAllowed()
        {
            return gameStateStorage.GetCurrentState() == GameState.Paused;
        }

        public void ChangeState()
        {
            gameStateStorage.SetCurrentState(GameState.InProgress);
            gameStartListeners.ForEach(listener => listener.OnGameResume());
            Debug.Log("Game resumed!");
        }

        private void AddListener(IGameListener gameListener)
        {
            if (gameListener is IGamePauseListener gamePauseListener)
            {
                gameStartListeners.Add(gamePauseListener);
            }
        }
    }
}