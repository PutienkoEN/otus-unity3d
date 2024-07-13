using Zenject;

namespace ShootEmUp
{
    public class PlayerController
    {
        private readonly GameManager gameManager;
        private readonly InputManager inputManager;

        private readonly Unit player;
        private readonly PlayerAttackAgent playerAttackAgent;

        [Inject]
        public PlayerController(
            GameManager gameManager,
            InputManager inputManager,
            Unit player,
            PlayerAttackAgent playerAttackAgent)
        {
            this.gameManager = gameManager;
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

        private void OnCharacterDeath(Unit _) => gameManager.FinishGame();
    }
}