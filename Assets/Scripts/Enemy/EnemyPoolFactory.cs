using Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyPoolFactory : IFactory<ObjectPool<Unit>>
    {
        private readonly Transform world;
        private readonly Transform disabledContainer;

        private readonly EnemyFactory enemyFactory;

        [Inject]
        public EnemyPoolFactory(LevelProvider levelProvider, EnemyFactory enemyFactory)
        {
            world = levelProvider.worldContainer;
            disabledContainer = levelProvider.disabledContainerForEnemies;
            this.enemyFactory = enemyFactory;
        }

        public ObjectPool<Unit> Create()
        {
            return new ObjectPool<Unit>(CreateUnit, GetFromPull, ReturnToPull);
        }

        private Unit CreateUnit()
        {
            return enemyFactory.Create();
        }

        private void GetFromPull(Unit enemy)
        {
            enemy.transform.SetParent(world);
        }

        private void ReturnToPull(Unit enemy)
        {
            enemy.transform.SetParent(disabledContainer);
        }
    }
}