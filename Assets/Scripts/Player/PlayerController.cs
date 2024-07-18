using Zenject;

namespace ShootEmUp
{
    public class PlayerController
    {
        private readonly GameStateManager gameStateManager;
        private readonly InputManager inputManager;

        private readonly Unit player;
        private readonly PlayerAttackAgent playerAttackAgent;

        [Inject]
        public PlayerController(
            GameStateManager gameStateManager,
            InputManager inputManager,
            Unit player,
            PlayerAttackAgent playerAttackAgent)
        {
            this.gameStateManager = gameStateManager;
            this.inputManager = inputManager;
            this.player = player;
            this.playerAttackAgent = playerAttackAgent;

            OnCreate();
        }

        private void OnCreate()
        {
            player.Death += OnCharacterDeath;
            inputManager.MoveInput += player.MoveTo;
            inputManager.ShootInput += playerAttackAgent.Attack;
        }

        ~PlayerController()
        {
            OnDestroy();
        }

        private void OnDestroy()
        {
            player.Death -= OnCharacterDeath;
            inputManager.MoveInput -= player.MoveTo;
            inputManager.ShootInput -= playerAttackAgent.Attack;
        }

        private void OnCharacterDeath(Unit _) => gameStateManager.FinishGame();
    }
}