using Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyPoolFactory : IFactory<ObjectPool<Unit>>
    {
        private readonly Transform enabled;
        private readonly Transform disabled;
        private readonly EnemyFactory enemyFactory;

        [Inject]
        public EnemyPoolFactory(
            [Inject(Id = "enabled")] Transform enabled,
            [Inject(Id = "disabled")] Transform disabled,
            EnemyFactory enemyFactory)
        {
            this.enabled = enabled;
            this.disabled = disabled;
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
            enemy.transform.SetParent(enabled);
        }

        private void ReturnToPull(Unit enemy)
        {
            enemy.transform.SetParent(disabled);
        }
    }
}