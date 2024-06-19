using ShootEmUp;

namespace Components
{
    public interface IDamageable
    {
        public void TakeDamage(int damage, Team team);
    }
}