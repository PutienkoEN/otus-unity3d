using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class FinishGameStateHandler : IGameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly List<IGameFinishListener> gameFinishListeners = new();

        public FinishGameStateHandler(GameStateStorage gameStateStorage)
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
            gameStateStorage.SetCurrentState(GameState.Finished);
            gameFinishListeners.ForEach(listener => listener.OnGameFinish());
            Debug.Log("Game finished!");
        }

        private void AddListener(IGameListener gameListener)
        {
            if (gameListener is IGameFinishListener gameStartListener)
            {
                gameFinishListeners.Add(gameStartListener);
            }
        }
    }
}