using Components;
using Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletPoolFactory : IFactory<ObjectPool<Bullet>>
    {
        private readonly Transform world;
        private readonly Transform disabledContainer;

        private readonly BulletFactory bulletFactory;
        private readonly DamageComponent damageComponent;
        private readonly BulletLocationObserver bulletLocationObserver;

        [Inject]
        public BulletPoolFactory(
            LevelProvider levelProvider,
            BulletFactory bulletFactory,
            DamageComponent damageComponent,
            BulletLocationObserver bulletLocationObserver)
        {
            world = levelProvider.worldContainer;
            disabledContainer = levelProvider.disabledContainerForBullets;
            this.bulletFactory = bulletFactory;
            this.damageComponent = damageComponent;
            this.bulletLocationObserver = bulletLocationObserver;
        }

        public ObjectPool<Bullet> Create()
        {
            return new ObjectPool<Bullet>(CreateBullet, GetFromPull, BackToPull);
        }

        private Bullet CreateBullet()
        {
            var bullet = bulletFactory.Create();
            return bullet;
        }

        private void GetFromPull(Bullet entity)
        {
            entity.transform.SetParent(world);
            entity.CollisionEntered += OnBulletHit;
            bulletLocationObserver.Subscribe(entity);
        }

        private void BackToPull(Bullet entity)
        {
            entity.transform.SetParent(disabledContainer);
            entity.CollisionEntered -= OnBulletHit;
            bulletLocationObserver.Unsubscribe(entity);

            // TODO: add remove from observers
        }

        private void OnBulletHit(Bullet bullet, Collision2D collision)
        {
            bullet.CollisionEntered -= OnBulletHit;
            bullet.transform.SetParent(disabledContainer);
            damageComponent.DealDamage(bullet, collision.gameObject);
        }
    }
}