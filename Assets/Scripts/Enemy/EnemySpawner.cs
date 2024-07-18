using Pool;
using Zenject;

namespace ShootEmUp
{
    public class EnemySpawner
    {
        private readonly ObjectPool<Unit> enemyPool;

        private readonly EnemyPositions enemyPositions;
        private readonly Unit character;

        private readonly EnemyMoveHandler enemyMoveHandler;
        private readonly EnemyAttackHandler enemyAttackHandler;

        [Inject]
        public EnemySpawner(
            ObjectPool<Unit> enemyPool,
            EnemyPositions enemyPositions,
            Unit character,
            EnemyMoveHandler enemyMoveHandler,
            EnemyAttackHandler enemyAttackHandler)
        {
            this.enemyPool = enemyPool;
            this.enemyPositions = enemyPositions;
            this.character = character;
            this.enemyMoveHandler = enemyMoveHandler;
            this.enemyAttackHandler = enemyAttackHandler;

            this.enemyMoveHandler.TargetReached += StartAttack;
        }

        private void StartAttack(Unit unit)
        {
            enemyAttackHandler.Attack(unit, character);
        }

        ~EnemySpawner()
        {
            enemyMoveHandler.TargetReached -= StartAttack;
        }

        public void Spawn()
        {
            var spawnPosition = enemyPositions.RandomSpawnPosition();
            var attackPosition = enemyPositions.RandomAttackPosition();

            var enemy = GetFromPool();
            enemy.transform.position = spawnPosition.position;

            enemyMoveHandler.Move(enemy, attackPosition);
        }

        private Unit GetFromPool()
        {
            var enemy = enemyPool.Get();
            enemy.Death += PutBackToPool;

            return enemy;
        }

        private void PutBackToPool(Unit enemy)
        {
            enemyPool.Release(enemy);
            enemy.Death -= PutBackToPool;
        }
    }
}