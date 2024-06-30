using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class StartGameStateHandler : IGameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly GameStartTimer gameStartTimer;
        private readonly List<IGameStartListener> gameStartListeners = new();

        public StartGameStateHandler(GameStateStorage gameStateStorage, GameStartTimer gameStartTimer)
        {
            this.gameStateStorage = gameStateStorage;
            this.gameStartTimer = gameStartTimer;

            IGameListener.OnRegister += AddListener;
        }

        public bool IsAllowed()
        {
            return gameStateStorage.GetCurrentState() == GameState.None;
        }

        public void ChangeState()
        {
            gameStateStorage.SetCurrentState(GameState.AwaitStart);
            gameStartTimer.StartTimer(StartGame);
        }

        private void StartGame()
        {
            gameStateStorage.SetCurrentState(GameState.InProgress);
            gameStartListeners.ForEach(listener => listener.OnGameStart());
            Debug.Log("Game started!");
        }

        private void AddListener(IGameListener gameListener)
        {
            if (gameListener is IGameStartListener gameStartListener)
            {
                gameStartListeners.Add(gameStartListener);
            }
        }
    }
}