using Pool;
using ShootEmUp;
using Zenject;

namespace DI
{
    public class EnemySystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            EnemyPoolConfiguration();
            EnemySpawnerConfiguration();
        }

        private void EnemyPoolConfiguration()
        {
            Container
                .Bind<EnemyFactory>()
                .FromNew()
                .AsSingle();

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