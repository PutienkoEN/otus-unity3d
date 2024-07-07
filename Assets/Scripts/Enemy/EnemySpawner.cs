using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour, IGameUpdateListener
    {
        // Constant
        private EnemyPositions enemyPositions;
        private EnemyFactory enemyFactory;

        private int numberOfEnemiesToSpawn;
        private float spawnInterval;

        // Dynamic
        private int numberOfSpawnedEnemies;
        private float timLeftBeforeSpawn;

        [Inject]
        public void Construct(
            EnemyPositions enemyPositions,
            EnemyFactory enemyFactory,
            int numberOfEnemiesToSpawn,
            float spawnInterval)
        {
            this.enemyPositions = enemyPositions;
            this.enemyFactory = enemyFactory;
            this.numberOfEnemiesToSpawn = numberOfEnemiesToSpawn;
            this.spawnInterval = spawnInterval;
        }

        private void Awake()
        {
            IGameListener.Register(this);
            timLeftBeforeSpawn = spawnInterval;
        }

        public void OnUpdate(float deltaTime)
        {
            if (numberOfSpawnedEnemies >= numberOfEnemiesToSpawn)
            {
                return;
            }

            timLeftBeforeSpawn -= deltaTime;
            if (timLeftBeforeSpawn > 0)
            {
                return;
            }

            SpawnEnemy();
            timLeftBeforeSpawn = spawnInterval;
            numberOfSpawnedEnemies++;
        }

        private void SpawnEnemy()
        {
            var spawnPosition = enemyPositions.RandomSpawnPosition();
            var attackPosition = enemyPositions.RandomAttackPosition();
            enemyFactory.Create(spawnPosition, attackPosition);
        }
    }
}