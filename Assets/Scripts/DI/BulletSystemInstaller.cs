using Pool;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class BulletSystemInstaller : MonoInstaller
    {
        [SerializeField] private Bullet bulletPrefab;

        [SerializeField] private Transform world;
        [SerializeField] private Transform bulletPoolDisabled;

        [SerializeField] private LevelBounds levelBounds;

        public override void InstallBindings()
        {
            Container
                .Bind<Bullet>()
                .FromInstance(bulletPrefab)
                .AsSingle()
                .WhenInjectedInto(typeof(BulletFactory));

            Container
                .Bind<BulletFactory>()
                .AsSingle();

            Container
                .Bind<LevelBounds>()
                .FromInstance(levelBounds)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<BulletLocationObserver>()
                .AsSingle();

            Container
                .Bind<BulletLocationController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInstance(world)
                .WithId("enabled")
                .WhenInjectedInto(typeof(BulletPoolFactory));

            Container
                .BindInstance(bulletPoolDisabled)
                .WithId("disabled")
                .WhenInjectedInto(typeof(BulletPoolFactory));

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