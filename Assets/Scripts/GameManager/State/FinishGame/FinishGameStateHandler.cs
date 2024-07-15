using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class FinishGameStateHandler : IGameStateHandler, IGameStateObserver<IGameFinishListener>
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly List<IGameFinishListener> gameFinishListeners = new();

        [Inject]
        public FinishGameStateHandler(GameStateStorage gameStateStorage)
        {
            this.gameStateStorage = gameStateStorage;
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

        public void Observe(IGameFinishListener listener)
        {
            gameFinishListeners.Add(listener);
        }
    }
}