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
        [SerializeField] private BulletPool bulletPool;

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


            EnemyConfiguration();
            // PlayerConfiguration();
            BulletConfiguration();
        }

        private void EnemyConfiguration()
        {
            EnemyPoolConfiguration();
            EnemySpawnerConfiguration();
        }

        private void EnemySpawnerConfiguration()
        {
            // Container
            //     .Bind<int>()
            //     .FromInstance(numberOfEnemiesToSpawn)
            //     .AsTransient()
            //     .WhenInjectedInto(typeof(EnemySpawner));
            //
            // Container
            //     .Bind<float>()
            //     .FromInstance(spawnInterval)
            //     .AsTransient()
            //     .WhenInjectedInto(typeof(EnemySpawner));

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

            // Container
            //     .Bind<EnemyTimedSpawner>()
            //     .AsSingle()
            //     .NonLazy();
        }

        private void EnemyPoolConfiguration()
        {
            // Container
            //     .BindInstance(enemyPrefab)
            //     .AsTransient()
            //     .WhenInjectedInto<EnemyFactory>();

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

        private void BulletConfiguration()
        {
            Container
                .Bind<BulletPool>()
                .FromInstance(bulletPool)
                .AsSingle();

            Container
                .Bind<DamageComponent>()
                .AsSingle();

            Container
                .Bind<BulletFactory>()
                .AsSingle();
        }

        // private void PlayerConfiguration()
        // {
        //     Container
        //         .Bind<InputManager>()
        //         .FromComponentInHierarchy()
        //         .AsSingle();
        //
        //     Container
        //         .Bind<Unit>()
        //         .FromInstance(character)
        //         .AsTransient()
        //         .WhenInjectedInto(typeof(PlayerAttackAgent), typeof(PlayerController));
        //
        //     Container
        //         .Bind<GameObject>()
        //         .FromInstance(characterTarget)
        //         .AsSingle()
        //         .WhenInjectedInto(typeof(PlayerAttackAgent));
        //
        //     Container
        //         .Bind<PlayerAttackAgent>()
        //         .AsSingle();
        //
        //     Container
        //         .Bind<PlayerController>()
        //         .FromNew()
        //         .AsSingle()
        //         .NonLazy();
        // }
    }
}