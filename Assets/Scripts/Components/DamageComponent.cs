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
            var damageable = gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(bullet.Damage, bullet.Team);
        }
    }
}