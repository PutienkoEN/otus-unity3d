using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InputManager inputManager;

        [SerializeField] private Unit player;
        [SerializeField] private PlayerAttackAgent playerAttackAgent;

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