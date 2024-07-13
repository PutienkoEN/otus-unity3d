using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class BulletSystemInstaller : MonoInstaller
    {
        [SerializeField] private BulletPool bulletPool;

        public override void InstallBindings()
        {
            Container
                .Bind<BulletPool>()
                .FromInstance(bulletPool)
                .AsSingle();

            Container
                .Bind<BulletSpawner>()
                .AsSingle();
        }
    }
}