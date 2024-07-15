using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyTimedSpawner :
        ITickable,
        IGamePauseListener,
        IGameFinishListener
    {
        private readonly EnemySpawner enemySpawner;
        private readonly Settings settings;

        // Dynamic
        private int numberOfSpawnedEnemies;
        private float timLeftBeforeSpawn;

        private bool spawnerEnabled = true;

        [Inject]
        public EnemyTimedSpawner(EnemySpawner enemySpawner, Settings settings)
        {
            this.enemySpawner = enemySpawner;
            this.settings = settings;

            timLeftBeforeSpawn = settings.spawnInterval;
        }

        public void Tick()
        {
            if (!spawnerEnabled)
            {
                return;
            }

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

        public void OnGamePause()
        {
            spawnerEnabled = false;
        }

        public void OnGameResume()
        {
            spawnerEnabled = true;
        }

        public void OnGameFinish()
        {
            OnGamePause();
        }

        [Serializable]
        public class Settings
        {
            public int numberOfEnemies;
            public float spawnInterval;
        }
    }
}