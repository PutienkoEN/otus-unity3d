using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyTimedSpawner : ITickable
    {
        private readonly EnemySpawner enemySpawner;
        private readonly Settings settings;

        // Dynamic
        private int numberOfSpawnedEnemies;
        private float timLeftBeforeSpawn;

        [Inject]
        public EnemyTimedSpawner(EnemySpawner enemySpawner, Settings settings)
        {
            this.enemySpawner = enemySpawner;
            this.settings = settings;

            timLeftBeforeSpawn = settings.spawnInterval;
        }

        public void Tick()
        {
            if (numberOfSpawnedEnemies >= settings.numberOfEnemies)
            {
                return;
            }

            timLeftBeforeSpawn -= Time.deltaTime;
            if (timLeftBeforeSpawn > 0)
            {
                return;
            }

            enemySpawner.Spawn();
            timLeftBeforeSpawn = settings.spawnInterval;
            numberOfSpawnedEnemies++;
        }

        [Serializable]
        public class Settings
        {
            public int numberOfEnemies;
            public float spawnInterval;
        }
    }
}