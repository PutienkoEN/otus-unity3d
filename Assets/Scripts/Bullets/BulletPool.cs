using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPool : CachedObjectPool<Bullet>
    {
        [Header("Bullet pool configuration")] [SerializeField]
        private LevelBounds levelBounds;

        private readonly List<Bullet> activeBulletsToCheckForBoundaries = new();

        private void FixedUpdate()
        {
            activeBulletsToCheckForBoundaries.Clear();
            activeBulletsToCheckForBoundaries.AddRange(ActivePoolEntries);

            for (int i = 0, count = activeBulletsToCheckForBoundaries.Count; i < count; i++)
            {
                var bullet = activeBulletsToCheckForBoundaries[i];
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    Pool.Release(bullet);
                }
            }
        }

        protected override void AddToPull(Bullet bullet)
        {
            bullet.OnCollisionEntered += ReleaseFromPull;
        }

        protected override void RemoveFromPull(Bullet bullet)
        {
            bullet.OnCollisionEntered -= ReleaseFromPull;
        }

        private void ReleaseFromPull(Bullet bullet, Collision2D _) => Pool.Release(bullet);
    }
}