using Components;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Unit character;
        [SerializeField] private GameObject characterTarget;

        [SerializeField] private BulletPool bulletPool;

        [SerializeField] private float targetReachedMagnitude;
        [SerializeField] private int numberOfEnemiesToSpawn;
        [SerializeField] private float spawnInterval;

        public override void InstallBindings()
        {
            Container
                .Bind<GameManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            EnemyConfiguration();
            PlayerConfiguration();
            BulletConfiguration();
        }

        private void EnemyConfiguration()
        {
            Container
                .Bind<EnemyPool>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<float>()
                .FromInstance(targetReachedMagnitude)
                .AsTransient()
                .WhenInjectedInto(typeof(EnemyFactory));


            Container
                .Bind<int>()
                .FromInstance(numberOfEnemiesToSpawn)
                .AsTransient()
                .WhenInjectedInto(typeof(EnemySpawner));

            Container
                .Bind<float>()
                .FromInstance(spawnInterval)
                .AsTransient()
                .WhenInjectedInto(typeof(EnemySpawner));

            Container
                .Bind<EnemyFactory>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<EnemyPositions>()
                .FromComponentInHierarchy()
                .AsSingle();

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

        private void PlayerConfiguration()
        {
            Container
                .Bind<InputManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<Unit>()
                .FromInstance(character)
                .AsSingle()
                .WhenInjectedInto(typeof(PlayerAttackAgent), typeof(PlayerController), typeof(EnemyFactory));

            Container
                .Bind<GameObject>()
                .FromInstance(characterTarget)
                .AsSingle()
                .WhenInjectedInto(typeof(PlayerAttackAgent));

            Container
                .Bind<PlayerAttackAgent>()
                .AsSingle();

            Container
                .Bind<PlayerController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}