using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class WeaponComponent
    {
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private BulletConfig bulletConfig;

        [SerializeField] private Transform firePoint;

        public void Initialize(BulletSpawner bulletSpawner)
        {
            this.bulletSpawner = bulletSpawner;
        }

        public void Attack(Transform target)
        {
            var position = new Vector2(firePoint.position.x, firePoint.position.y);
            var direction = (new Vector2(target.position.x, target.position.y) - position).normalized;

            var bulletData = new BulletSpawner.BulletData
            {
                BulletConfig = bulletConfig,
                Position = position,
                Velocity = direction * bulletConfig.speed
            };

            bulletSpawner.SpawnBullet(bulletData);
        }
    }
}