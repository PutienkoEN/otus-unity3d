using System;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp
{
    public class BulletLocationObserver : IFixedTickable, IGamePauseListener, IGameFinishListener
    {
        public Action<Bullet> LocationExit;

        private readonly LevelBounds levelBounds;

        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cacheForActiveBullets = new();

        private bool enabled = true;

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
            // Prevent any action non bullet in case pause/finish
            if (!enabled)
            {
                return;
            }

            PutBulletsToCache();

            cacheForActiveBullets
                .FindAll(OutOfBounds)
                .ForEach(TriggerEventAndUnsubscribe);
        }

        private void PutBulletsToCache()
        {
            cacheForActiveBullets.Clear();
            cacheForActiveBullets.AddRange(activeBullets);
        }

        public void OnGamePause()
        {
            enabled = false;

            PutBulletsToCache();
            cacheForActiveBullets
                .ForEach(bullet => bullet.Stop());
        }

        public void OnGameResume()
        {
            enabled = true;

            PutBulletsToCache();
            cacheForActiveBullets
                .ForEach(bullet => bullet.Resume());
        }

        public void OnGameFinish()
        {
            OnGamePause();
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