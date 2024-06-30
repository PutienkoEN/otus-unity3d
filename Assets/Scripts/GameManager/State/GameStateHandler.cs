namespace ShootEmUp
{
    public class GameStateHandler
    {
        private readonly GameStateStorage gameStateStorage;

        private readonly GameState stateToSet;
        private readonly GameState allowedToTransitionFrom;

        public GameStateHandler(
            GameStateStorage gameStateStorage,
            GameState stateToSet,
            GameState allowedToTransitionFrom)
        {
            this.gameStateStorage = gameStateStorage;
            this.stateToSet = stateToSet;
            this.allowedToTransitionFrom = allowedToTransitionFrom;
        }

        public bool IsNotAllowedTransition()
        {
            return allowedToTransitionFrom != gameStateStorage.GetCurrentState();
        }

        public virtual void ChangeState()
        {
            gameStateStorage.SetCurrentState(stateToSet);
        }
    }
}