using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyTimedSpawner : IGameUpdateListener
    {
        private readonly EnemySpawner enemySpawner;
        private readonly Settings settings;

        private int numberOfSpawnedEnemies;
        private float timLeftBeforeSpawn;

        [Inject]
        public EnemyTimedSpawner(EnemySpawner enemySpawner, Settings settings)
        {
            this.enemySpawner = enemySpawner;
            this.settings = settings;

            timLeftBeforeSpawn = settings.spawnInterval;
        }

        public void OnUpdate(float deltaTime)
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