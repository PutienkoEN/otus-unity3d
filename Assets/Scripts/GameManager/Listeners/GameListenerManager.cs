using System.Collections.Generic;

namespace ShootEmUp
{
    public class GameListenerManager
    {
        private readonly List<IGameListener> gameListeners = new();

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

        private void AddListener(IGameListener gameListener)
        {
            gameListeners.Add(gameListener);
        }
    }
}