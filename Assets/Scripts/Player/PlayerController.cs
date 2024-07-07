using DI;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class PlayerController : MonoBehaviour
    {
        private GameManager gameManager;
        private InputManager inputManager;

        private Unit player;
        private PlayerAttackAgent playerAttackAgent;

        [Inject]
        public void Construct(
            GameManager gameManager,
            InputManager inputManager,
            [Inject(Id = SceneInstaller.Character)]
            Unit player,
            PlayerAttackAgent playerAttackAgent)
        {
            this.gameManager = gameManager;
            this.inputManager = inputManager;
            this.player = player;
            this.playerAttackAgent = playerAttackAgent;
        }

        private void OnEnable()
        {
            player.OnDeath += OnCharacterDeath;
            inputManager.OnMoveInput += player.MoveTo;
            inputManager.OnShootInput += playerAttackAgent.Attack;
        }

        private void OnDisable()
        {
            player.OnDeath -= OnCharacterDeath;
            inputManager.OnMoveInput -= player.MoveTo;
            inputManager.OnShootInput -= playerAttackAgent.Attack;
        }

        private void OnCharacterDeath(Unit _) => gameManager.FinishGame();
    }
}