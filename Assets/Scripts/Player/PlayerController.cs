using Zenject;

namespace ShootEmUp
{
    public class PlayerController
    {
        private readonly Unit character;

        private readonly GameStateManager gameStateManager;
        private readonly InputManager inputManager;
        private readonly PlayerAttackAgent playerAttackAgent;

        [Inject]
        public PlayerController(
            LevelProvider levelProvider,
            GameStateManager gameStateManager,
            InputManager inputManager,
            PlayerAttackAgent playerAttackAgent)
        {
            character = levelProvider.characterObject;

            this.gameStateManager = gameStateManager;
            this.inputManager = inputManager;

            this.playerAttackAgent = playerAttackAgent;

            OnCreate();
        }

        private void OnCreate()
        {
            character.Death += OnCharacterDeath;
            inputManager.MoveInput += character.MoveTo;
            inputManager.ShootInput += playerAttackAgent.Attack;
        }

        ~PlayerController()
        {
            OnDestroy();
        }

        private void OnDestroy()
        {
            character.Death -= OnCharacterDeath;
            inputManager.MoveInput -= character.MoveTo;
            inputManager.ShootInput -= playerAttackAgent.Attack;
        }

        private void OnCharacterDeath(Unit _) => gameStateManager.FinishGame();
    }
}