using Zenject;

namespace ShootEmUp
{
    public class GameStateManager
    {
        private readonly IGameStateHandler startGameStateHandler;
        private readonly IGameStateHandler finishGameStateHandler;

        private readonly PauseGameStateHandler pauseGameStateHandler;

        [Inject]
        public GameStateManager(
            StartGameStateHandler startGameStateHandler,
            FinishGameStateHandler finishGameStateHandler,
            PauseGameStateHandler pauseGameStateHandler)
        {
            this.startGameStateHandler = startGameStateHandler;
            this.finishGameStateHandler = finishGameStateHandler;
            this.pauseGameStateHandler = pauseGameStateHandler;
        }

        public void StartGame()
        {
            if (startGameStateHandler.IsAllowed())
            {
                startGameStateHandler.ChangeState();
            }
        }

        public void FinishGame()
        {
            if (finishGameStateHandler.IsAllowed())
            {
                finishGameStateHandler.ChangeState();
            }
        }

        public void PauseGame()
        {
            if (pauseGameStateHandler.IsPauseAllowed())
            {
                pauseGameStateHandler.PauseGame();
            }
        }

        public void ResumeGame()
        {
            if (pauseGameStateHandler.IsResumeAllowed())
            {
                pauseGameStateHandler.ResumeGame();
            }
        }
    }
}