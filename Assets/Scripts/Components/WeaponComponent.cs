using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public class WeaponComponent
    {
        // Configured in editor.
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private Transform firePoint;

        private BulletSpawner bulletSpawner;

        public void Construct(BulletSpawner bulletSpawner)
        {
            this.bulletSpawner = bulletSpawner;
        }

        public void Attack(Transform target)
        {
            var firePosition = GetFirePosition();
            var direction = GetTargetPosition(target, firePosition);

            var bulletData = new BulletData
            {
                BulletConfig = bulletConfig,
                Position = firePosition,
                Velocity = direction * bulletConfig.speed
            };

            bulletSpawner.SpawnBullet(bulletData);
        }

        private Vector2 GetFirePosition()
        {
            var firePointPosition = firePoint!.position;
            return new Vector2(firePointPosition.x, firePointPosition.y);
        }

        private static Vector2 GetTargetPosition(Transform target, Vector2 firePosition)
        {
            var position = target!.position;
            var targetPosition = new Vector2(position.x, position.y);

            var direction = targetPosition - firePosition;
            return direction.normalized;
        }
    }
}