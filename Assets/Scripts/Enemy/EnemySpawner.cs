using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private EnemyFactory enemyFactory;

        [SerializeField] private float numberOfEnemiesToSpawn;
        [SerializeField] private float spawnInterval;

        private int numberOfSpawnedEnemies;
        private float timLeftBeforeSpawn;

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
            enemyFactory.SpawnEnemy(spawnPosition, attackPosition);
        }
    }
}