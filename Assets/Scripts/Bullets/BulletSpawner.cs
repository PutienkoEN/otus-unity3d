using Pool;
using Zenject;

namespace ShootEmUp
{
    public class BulletSpawner
    {
        private readonly ObjectPool<Bullet> bulletPool;

        [Inject]
        public BulletSpawner(ObjectPool<Bullet> bulletPool)
        {
            this.bulletPool = bulletPool;
        }

        public void SpawnBullet(BulletData bulletData)
        {
            var bullet = bulletPool.Get();
            bullet.Shoot(bulletData);
        }
    }
}