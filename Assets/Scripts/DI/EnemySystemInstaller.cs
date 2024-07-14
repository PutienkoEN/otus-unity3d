using Pool;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class EnemySystemInstaller : MonoInstaller
    {
        [SerializeField] private Unit character;

        [SerializeField] private Unit enemyPrefab;
        [SerializeField] private Transform world;
        [SerializeField] private Transform enemyPoolDisabled;

        public override void InstallBindings()
        {
            EnemyPoolConfiguration();
            EnemySpawnerConfiguration();
        }

        private void EnemyPoolConfiguration()
        {
            Container
                .Bind<Unit>()
                .FromInstance(enemyPrefab)
                .WhenInjectedInto<EnemyFactory>();

            Container
                .Bind<EnemyFactory>()
                .FromNew()
                .AsSingle();

            Container
                .BindInstance(world)
                .WithId("enabled")
                .WhenInjectedInto(typeof(EnemyPoolFactory));

            Container
                .BindInstance(enemyPoolDisabled)
                .WithId("disabled")
                .WhenInjectedInto(typeof(EnemyPoolFactory));

            Container
                .Bind<ObjectPool<Unit>>()
                .FromFactory<EnemyPoolFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void EnemySpawnerConfiguration()
        {
            Container
                .Bind<EnemyPositions>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<Unit>()
                .FromInstance(character)
                .AsTransient()
                .WhenInjectedInto(typeof(EnemySpawner));

            Container
                .Bind<EnemySpawner>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EnemyMoveHandler>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EnemyAttackHandler>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EnemyTimedSpawner>()
                .AsSingle();
        }
    }
}