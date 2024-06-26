using System;
using ShootEmUp;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class DamageComponent
    {
        public void DealDamage(Bullet bullet, GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(bullet.Damage, bullet.Team);
            }
        }
    }
}