using Components;
using Pool;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletPoolFactory : IFactory<ObjectPool<Bullet>>
    {
        private readonly Transform enabled;
        private readonly Transform disabled;

        private readonly BulletFactory bulletFactory;
        private readonly DamageComponent damageComponent;
        private readonly BulletLocationObserver bulletLocationObserver;

        // Inject here once instead of bullet for each creation 
        private readonly IGameStateObserver<IGamePauseListener> pauseObserver;
        private readonly IGameStateObserver<IGameFinishListener> finishObserver;

        [Inject]
        public BulletPoolFactory(
            [Inject(Id = "enabled")] Transform enabled,
            [Inject(Id = "disabled")] Transform disabled,
            BulletFactory bulletFactory,
            DamageComponent damageComponent,
            BulletLocationObserver bulletLocationObserver,
            IGameStateObserver<IGamePauseListener> pauseObserver,
            IGameStateObserver<IGameFinishListener> finishObserver)
        {
            this.enabled = enabled;
            this.disabled = disabled;
            this.bulletFactory = bulletFactory;
            this.damageComponent = damageComponent;
            this.bulletLocationObserver = bulletLocationObserver;
            this.pauseObserver = pauseObserver;
            this.finishObserver = finishObserver;
        }

        public ObjectPool<Bullet> Create()
        {
            return new ObjectPool<Bullet>(CreateBullet, GetFromPull, BackToPull);
        }

        private Bullet CreateBullet()
        {
            var bullet = bulletFactory.Create();

            pauseObserver.Observe(bullet);
            finishObserver.Observe(bullet);

            return bullet;
        }

        private void GetFromPull(Bullet entity)
        {
            entity.transform.SetParent(enabled);
            entity.CollisionEntered += OnBulletHit;
            bulletLocationObserver.Subscribe(entity);
        }

        private void BackToPull(Bullet entity)
        {
            entity.transform.SetParent(disabled);
            entity.CollisionEntered -= OnBulletHit;
            bulletLocationObserver.Unsubscribe(entity);

            // TODO: add remove from observers
        }

        private void OnBulletHit(Bullet bullet, Collision2D collision)
        {
            bullet.CollisionEntered -= OnBulletHit;
            bullet.transform.SetParent(disabled);
            damageComponent.DealDamage(bullet, collision.gameObject);
        }
    }
}