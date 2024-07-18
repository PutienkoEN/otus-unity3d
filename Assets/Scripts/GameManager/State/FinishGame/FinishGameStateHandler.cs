using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class FinishGameStateHandler : IGameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly List<IGameFinishListener> gameFinishListeners;

        [Inject]
        public FinishGameStateHandler(GameStateStorage gameStateStorage, List<IGameFinishListener> gameFinishListeners)
        {
            this.gameStateStorage = gameStateStorage;
            this.gameFinishListeners = gameFinishListeners;
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
    }
}