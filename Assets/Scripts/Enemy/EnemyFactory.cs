using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyFactory
    {
        private EnemyPool enemyPool;
        private Unit character;
        private float targetReachedMagnitude;

        [Inject]
        public void Construct(EnemyPool enemyPool, Unit character, float targetReachedMagnitude)
        {
            this.enemyPool = enemyPool;
            this.character = character;
            this.targetReachedMagnitude = targetReachedMagnitude;
        }

        public void SpawnEnemy(Transform spawnPosition, Transform moveToPosition)
        {
            var enemy = enemyPool.Get();

            InitializeMoveAgent(enemy, spawnPosition, moveToPosition);
            InitializeAttackAgent(enemy);
        }

        private void InitializeMoveAgent(Unit enemy, Transform spawnPosition, Transform moveToPosition)
        {
            if (!enemy.TryGetComponent(out EnemyMoveAgent moveAgent))
            {
                throw new MissingComponentException($"Component {typeof(EnemyMoveAgent)} for {enemy} was not found.");
            }

            enemy.transform.position = spawnPosition.position;
            moveAgent.Initialize(enemy.transform, moveToPosition, targetReachedMagnitude);
        }

        private void InitializeAttackAgent(Unit enemy)
        {
            if (!enemy.TryGetComponent(out EnemyAttackAgent attackAgent))
            {
                throw new MissingComponentException($"Component {typeof(EnemyAttackAgent)} for {enemy} was not found.");
            }

            attackAgent.Initialize(enemy, character);
        }
    }
}