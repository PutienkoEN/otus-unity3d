using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class PauseGameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly List<IGamePauseListener> gamePauseListener;

        [Inject]
        public PauseGameStateHandler(GameStateStorage gameStateStorage, List<IGamePauseListener> gamePauseListener)
        {
            this.gameStateStorage = gameStateStorage;
            this.gamePauseListener = gamePauseListener;
        }

        public bool IsPauseAllowed()
        {
            return gameStateStorage.GetCurrentState() == GameState.InProgress;
        }

        public bool IsResumeAllowed()
        {
            return gameStateStorage.GetCurrentState() == GameState.Paused;
        }

        public void PauseGame()
        {
            gameStateStorage.SetCurrentState(GameState.Paused);
            gamePauseListener.ForEach(listener => listener.OnGamePause());
            Debug.Log("Game paused!");
        }

        public void ResumeGame()
        {
            gameStateStorage.SetCurrentState(GameState.InProgress);
            gamePauseListener.ForEach(listener => listener.OnGameResume());
            Debug.Log("Game resumed!");
        }
    }
}