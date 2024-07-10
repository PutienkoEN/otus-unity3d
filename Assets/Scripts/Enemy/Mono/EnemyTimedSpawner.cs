using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyTimedSpawner : MonoBehaviour
    {
        private EnemySpawner enemySpawner;
        private Settings settings;

        // Dynamic
        private int numberOfSpawnedEnemies;
        private float timLeftBeforeSpawn;

        [Inject]
        public void Construct(EnemySpawner enemySpawner, Settings settings)
        {
            this.enemySpawner = enemySpawner;
            this.settings = settings;
            timLeftBeforeSpawn = settings.spawnInterval;
        }

        public void Update()
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