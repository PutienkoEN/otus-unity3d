using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    [Serializable]
    public class WeaponComponent
    {
        [FormerlySerializedAs("bulletSpawner")] [SerializeField] private BulletFactory bulletFactory;
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private Transform firePoint;

        public void Initialize(BulletFactory bulletFactory)
        {
            this.bulletFactory = bulletFactory;
        }

        public void Attack(Transform target)
        {
            var firePosition = GetFirePosition();
            var direction = GetTargetPosition(target, firePosition);

            var bulletData = new BulletFactory.BulletData
            {
                BulletConfig = bulletConfig,
                Position = firePosition,
                Velocity = direction * bulletConfig.speed
            };

            bulletFactory.SpawnBullet(bulletData);
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