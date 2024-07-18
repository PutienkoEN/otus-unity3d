using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class StartGameStateHandler : IGameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly GameStartTimer gameStartTimer;
        private readonly List<IGameStartListener> gameStartListeners = new();

        [Inject]
        public StartGameStateHandler(GameStateStorage gameStateStorage, GameStartTimer gameStartTimer)
        {
            this.gameStateStorage = gameStateStorage;
            this.gameStartTimer = gameStartTimer;
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
    }
}