using Pool;
using Zenject;

namespace ShootEmUp
{
    /**
     * This controller required to bind BulletLocationObserver and BulletPoolFactory.
     * Since we can't map  BulletLocationObserver to BulletPoolFactory directly, since it will create circular dependency.
     * Since Observer need to put entities back to pool, but also need to look for "active" entities,
     * and pool needs observer to subscribe and unsubscribe. This controllers allows remove circular dependency.
     */
    public class BulletLocationController
    {
        private readonly BulletLocationObserver bulletLocationObserver;
        private readonly ObjectPool<Bullet> bulletPool;

        [Inject]
        public BulletLocationController(BulletLocationObserver bulletLocationObserver, ObjectPool<Bullet> bulletPool)
        {
            this.bulletLocationObserver = bulletLocationObserver;
            this.bulletPool = bulletPool;

            this.bulletLocationObserver.LocationExit += bulletPool.Release;
        }

        ~BulletLocationController()
        {
            bulletLocationObserver.LocationExit -= bulletPool.Release;
        }
    }
}