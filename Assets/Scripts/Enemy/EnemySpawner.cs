using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPool enemyPool;
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Unit character;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var enemy = enemyPool.Get();

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().Initialize(enemy.transform, attackPosition.transform);
            enemy.GetComponent<EnemyAttackAgent>().Initialize(enemy, character);
        }
    }
}