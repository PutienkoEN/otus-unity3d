using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour, IGamePauseListener, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private EnemyPool enemyPool;
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Unit character;
        [SerializeField] private float targetReachedMagnitude;

        private void Awake()
        {
            IGameListener.Register(this);
            enabled = false;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (enabled)
                {
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            var enemy = enemyPool.Get();

            InitializeEnemyPosition(enemy);
            InitializeMoveAgent(enemy);
            InitializeAttackAgent(enemy);
        }

        private void InitializeEnemyPosition(Unit enemy)
        {
            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
        }

        private void InitializeMoveAgent(Unit enemy)
        {
            if (!enemy.TryGetComponent(out EnemyMoveAgent moveAgent))
            {
                throw new MissingComponentException($"Component {typeof(EnemyMoveAgent)} for {enemy} was not found.");
            }

            var attackPosition = enemyPositions.RandomAttackPosition();
            moveAgent.Initialize(enemy.transform, attackPosition.transform, targetReachedMagnitude);
        }

        private void InitializeAttackAgent(Unit enemy)
        {
            if (!enemy.TryGetComponent(out EnemyAttackAgent attackAgent))
            {
                throw new MissingComponentException($"Component {typeof(EnemyAttackAgent)} for {enemy} was not found.");
            }

            attackAgent.Initialize(enemy, character);
        }

        public void OnGamePause()
        {
            enabled = false;
        }

        public void OnGameResume()
        {
            enabled = true;
        }

        public void OnGameStart()
        {
            enabled = true;
        }

        public void OnGameFinish()
        {
            enabled = false;
        }
    }
}