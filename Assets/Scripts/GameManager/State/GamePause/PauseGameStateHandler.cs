using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class PauseGameStateHandler : IGameStateObserver<IGamePauseListener>
    {
        private readonly GameStateStorage gameStateStorage;
        private readonly List<IGamePauseListener> gamePauseListener = new();
        private readonly List<IGamePauseListener> gameResumeListener = new();

        [Inject]
        public PauseGameStateHandler(GameStateStorage gameStateStorage)
        {
            this.gameStateStorage = gameStateStorage;
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
            gameResumeListener.ForEach(listener => listener.OnGameResume());
            Debug.Log("Game resumed!");
        }

        public void Observe(IGamePauseListener listener)
        {
            gamePauseListener.Add(listener);
            gameResumeListener.Add(listener);
        }
    }
}