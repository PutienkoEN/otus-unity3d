using Pool;
using ShootEmUp;
using Zenject;

namespace DI
{
    public class BulletSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<LevelBounds>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<BulletFactory>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<BulletLocationObserver>()
                .AsSingle();

            Container
                .Bind<BulletLocationController>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ObjectPool<Bullet>>()
                .FromFactory<BulletPoolFactory>()
                .AsSingle();

            Container
                .Bind<BulletSpawner>()
                .AsSingle();
        }
    }
}