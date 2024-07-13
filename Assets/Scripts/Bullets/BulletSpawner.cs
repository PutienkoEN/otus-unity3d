using Components;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletSpawner
    {
        private BulletPool bulletPool;
        private DamageComponent damageComponent;

        [Inject]
        public void Construct(BulletPool bulletPool, DamageComponent damageComponent)
        {
            this.bulletPool = bulletPool;
            this.damageComponent = damageComponent;
        }

        public void SpawnBullet(BulletData bulletData)
        {
            var bullet = bulletPool.Get();
            ConfigureBullet(bullet, bulletData);
        }

        private void ConfigureBullet(Bullet bullet, BulletData bulletData)
        {
            bullet.CollisionEntered += OnBulletHit;
            bullet.Shoot(bulletData);
        }

        private void OnBulletHit(Bullet bullet, Collision2D collision)
        {
            bullet.CollisionEntered -= OnBulletHit;
            damageComponent.DealDamage(bullet, collision.gameObject);
        }
    }
}