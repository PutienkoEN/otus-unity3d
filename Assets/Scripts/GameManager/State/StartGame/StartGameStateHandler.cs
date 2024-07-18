using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    /**
     * Removed listeners, since for Start game we only wait for timer and then change state.
     */
    public class StartGameStateHandler : IGameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly GameStartTimer gameStartTimer;

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
            Debug.Log("Game started!");
        }
    }
}