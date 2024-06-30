using UnityEngine;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStateStorage gameStateStorage;

        private IGameStateHandler startGameStateHandler;
        private IGameStateHandler pauseGameStateHandler;
        private IGameStateHandler resumeGameStateHandler;
        private IGameStateHandler finishGameStateHandler;

        private void Awake()
        {
            var gameStartTimer = FindObjectOfType<GameStartTimer>();
            startGameStateHandler = new StartGameStateHandler(gameStateStorage, gameStartTimer);
            pauseGameStateHandler = new PauseGameStateHandler(gameStateStorage);
            resumeGameStateHandler = new ResumeGameStateHandler(gameStateStorage);
            finishGameStateHandler = new FinishGameStateHandler(gameStateStorage);
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