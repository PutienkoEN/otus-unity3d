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

        public void TriggerPauseListeners()
        {
            gameListeners
                .FindAll(listener => listener is IGamePauseListener)
                .ConvertAll(listener => (IGamePauseListener)listener)
                .ForEach(listener => listener.OnGamePause());
        }

        private void AddListener(IGameListener gameListener)
        {
            gameListeners.Add(gameListener);
        }
    }
}