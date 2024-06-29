using Components;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private DamageComponent damageComponent = new();

        public void SpawnBullet(BulletData bulletData)
        {
            var bullet = bulletPool.Get();
            ConfigureBullet(bullet, bulletData);
        }

        private void ConfigureBullet(Bullet bullet, BulletData bulletData)
        {
            bullet.OnCollisionEntered += OnBulletHit;
            bullet.Shoot(bulletData);
        }

        private void OnBulletHit(Bullet bullet, Collision2D collision)
        {
            bullet.OnCollisionEntered -= OnBulletHit;
            damageComponent.DealDamage(bullet, collision.gameObject);
        }

        public struct BulletData
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public BulletConfig BulletConfig;
        }
    }
}