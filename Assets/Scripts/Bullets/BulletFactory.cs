using Zenject;

namespace ShootEmUp
{
    public class BulletFactory : IFactory<Bullet>
    {
        private readonly Bullet bulletPrefab;
        private readonly DiContainer diContainer;

        [Inject]
        public BulletFactory(LevelProvider levelProvider, DiContainer diContainer)
        {
            bulletPrefab = levelProvider.bulletPrefab;
            this.diContainer = diContainer;
        }

        public Bullet Create()
        {
            return diContainer.InstantiatePrefabForComponent<Bullet>(bulletPrefab);
        }
    }
}