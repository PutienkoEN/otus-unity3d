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
            player.OnDeath += OnCharacterDeath;
            inputManager.OnMoveInput += player.MoveTo;
            inputManager.OnShootInput += playerAttackAgent.Attack;
        }

        ~PlayerController()
        {
            OnDestroy();
        }

        private void OnDestroy()
        {
            player.OnDeath -= OnCharacterDeath;
            inputManager.OnMoveInput -= player.MoveTo;
            inputManager.OnShootInput -= playerAttackAgent.Attack;
        }

        private void OnCharacterDeath(Unit _) => gameStateManager.FinishGame();
    }
}