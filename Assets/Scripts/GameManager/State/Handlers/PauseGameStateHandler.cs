using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PauseGameStateHandler : IGameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly List<IGamePauseListener> gameStartListeners = new();

        public PauseGameStateHandler(GameStateStorage gameStateStorage)
        {
            this.gameStateStorage = gameStateStorage;
            IGameListener.OnRegister += AddListener;
        }

        public bool IsAllowed()
        {
            return gameStateStorage.GetCurrentState() == GameState.InProgress;
        }

        public void ChangeState()
        {
            gameStateStorage.SetCurrentState(GameState.Paused);
            gameStartListeners.ForEach(listener => listener.OnGamePause());
            Debug.Log("Game paused!");
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