using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameListenerManager
    {
        private readonly List<IGameListener> gameListeners = new();
        private readonly List<IGameUpdateListener> gameUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> fixedUpdateListeners = new();

        public GameListenerManager()
        {
            IGameListener.OnRegister += AddListener;
        }

        public void TriggerGamePauseListeners()
        {
            gameListeners
                .FindAll(listener => listener is IGamePauseListener)
                .ConvertAll(listener => (IGamePauseListener)listener)
                .ForEach(listener => listener.OnGamePause());
        }

        public void TriggerGameResumeListeners()
        {
            gameListeners
                .FindAll(listener => listener is IGamePauseListener)
                .ConvertAll(listener => (IGamePauseListener)listener)
                .ForEach(listener => listener.OnGameResume());
        }

        public void TriggerGameStartListeners()
        {
            gameListeners
                .FindAll(listener => listener is IGameStartListener)
                .ConvertAll(listener => (IGameStartListener)listener)
                .ForEach(listener => listener.OnGameStart());
        }

        public void TriggerGameFinishListeners()
        {
            gameListeners
                .FindAll(listener => listener is IGameFinishListener)
                .ConvertAll(listener => (IGameFinishListener)listener)
                .ForEach(listener => listener.OnGameFinish());
        }

        public void TriggerGameUpdateListeners()
        {
            var deltaTime = Time.deltaTime;
            gameUpdateListeners.ForEach(listener => listener.OnUpdate(deltaTime));
        }

        public void TriggerGameFixedUpdateListeners()
        {
            var deltaTime = Time.fixedDeltaTime;
            fixedUpdateListeners.ForEach(listener => listener.OnFixedUpdate(deltaTime));
        }

        private void AddListener(IGameListener gameListener)
        {
            switch (gameListener)
            {
                case IGameFixedUpdateListener fixedUpdateListener:
                    fixedUpdateListeners.Add(fixedUpdateListener);
                    break;
                case IGameUpdateListener gameUpdateListener:
                    gameUpdateListeners.Add(gameUpdateListener);
                    break;
            }

            if (gameListener is IGameStartListener or IGamePauseListener or IGameFinishListener)
            {
                gameListeners.Add(gameListener);
            }
        }
    }
}