using Pool;

namespace ShootEmUp
{
    public class EnemyPool : CachedObjectPool<Unit>
    {
        protected override void AddToPull(Unit enemy)
        {
            enemy.OnDeath += Pool.Release;
        }

        protected override void RemoveFromPull(Unit enemy)
        {
            enemy.OnDeath -= Pool.Release;
        }
    }
}