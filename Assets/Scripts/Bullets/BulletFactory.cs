using Zenject;

namespace ShootEmUp
{
    public class BulletFactory : IFactory<Bullet>
    {
        private readonly Bullet bulletPrefab;
        private readonly DiContainer diContainer;

        [Inject]
        public BulletFactory(Bullet bulletPrefab, DiContainer diContainer)
        {
            this.bulletPrefab = bulletPrefab;
            this.diContainer = diContainer;
        }

        public Bullet Create()
        {
            return diContainer.InstantiatePrefabForComponent<Bullet>(bulletPrefab);
        }
    }
}