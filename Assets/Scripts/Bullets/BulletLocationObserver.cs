using System;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp
{
    public class BulletLocationObserver : IFixedTickable
    {
        public Action<Bullet> LocationExit;

        private readonly LevelBounds levelBounds;

        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cacheForActiveBullets = new();

        [Inject]
        public BulletLocationObserver(LevelBounds levelBounds)
        {
            this.levelBounds = levelBounds;
        }

        public void Subscribe(Bullet bullet)
        {
            activeBullets.Add(bullet);
        }

        public void Unsubscribe(Bullet bullet)
        {
            activeBullets.Remove(bullet);
        }

        public void FixedTick()
        {
            cacheForActiveBullets.Clear();
            cacheForActiveBullets.AddRange(activeBullets);

            cacheForActiveBullets
                .FindAll(OutOfBounds)
                .ForEach(TriggerEventAndUnsubscribe);
        }

        private bool OutOfBounds(Bullet bullet)
        {
            return !levelBounds.InBounds(bullet.transform.position);
        }

        private void TriggerEventAndUnsubscribe(Bullet bullet)
        {
            LocationExit?.Invoke(bullet);
            Unsubscribe(bullet);
        }
    }
}