using Zenject;

namespace ShootEmUp
{
    public class GameManager
    {
        private readonly IGameStateHandler startGameStateHandler;
        private readonly IGameStateHandler pauseGameStateHandler;
        private readonly IGameStateHandler resumeGameStateHandler;
        private readonly IGameStateHandler finishGameStateHandler;

        [Inject]
        public GameManager(
            StartGameStateHandler startGameStateHandler,
            PauseGameStateHandler pauseGameStateHandler,
            ResumeGameStateHandler resumeGameStateHandler,
            FinishGameStateHandler finishGameStateHandler)
        {
            this.startGameStateHandler = startGameStateHandler;
            this.pauseGameStateHandler = pauseGameStateHandler;
            this.resumeGameStateHandler = resumeGameStateHandler;
            this.finishGameStateHandler = finishGameStateHandler;
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
            if (pauseGameStateHandler.IsAllowed())
            {
                pauseGameStateHandler.ChangeState();
            }
        }

        public void ResumeGame()
        {
            if (resumeGameStateHandler.IsAllowed())
            {
                resumeGameStateHandler.ChangeState();
            }
        }
    }
}