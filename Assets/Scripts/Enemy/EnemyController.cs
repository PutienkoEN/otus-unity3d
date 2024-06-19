using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        [SerializeField] private EnemyMoveAgent enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent enemyAttackAgent;

        private void OnEnable()
        {
            enemyMoveAgent.OnTargetReached += enemyAttackAgent.EnableAttack;
            enemyMoveAgent.OnMove += unit.MoveTo;

            enemyAttackAgent.OnAttack += unit.Attack;
        }

        private void OnDisable()
        {
            enemyMoveAgent.OnTargetReached -= enemyAttackAgent.EnableAttack;
            enemyMoveAgent.OnMove -= unit.MoveTo;

            enemyAttackAgent.OnAttack -= unit.Attack;
        }
    }
}