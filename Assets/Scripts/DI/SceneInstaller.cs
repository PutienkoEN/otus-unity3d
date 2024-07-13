using Components;
using Pool;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Unit character;
        [SerializeField] private GameObject characterTarget;

        [SerializeField] private Unit enemyPrefab;

        [SerializeField] private Transform world;
        [SerializeField] private Transform enemyPoolDisabled;

        [SerializeField] private float targetReachedMagnitude;
        [SerializeField] private int numberOfEnemiesToSpawn;
        [SerializeField] private float spawnInterval;

        public override void InstallBindings()
        {
            Container
                .Bind<GameManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<InputManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<DamageComponent>()
                .AsSingle();

            EnemyConfiguration();
        }

        private void EnemyConfiguration()
        {
            EnemyPoolConfiguration();
            EnemySpawnerConfiguration();
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
                .Bind<EnemyMoveHandler>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<EnemyAttackHandler>()
                .FromComponentInHierarchy()
                .AsSingle();
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
    }
}