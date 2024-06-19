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
            var unit = gameObject.GetComponent<Unit>();
            if (unit)
            {
                DealDamage(bullet, unit);
            }
        }

        private static void DealDamage(Bullet bullet, Unit other)
        {
            if (bullet.Team == other.GetTeam())
            {
                return;
            }

            other.TakeDamage(bullet.Damage);
        }
    }
}