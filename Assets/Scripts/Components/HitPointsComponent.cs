using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class HitPointsComponent
    {
        [SerializeField] private int hitPoints;

        public bool IsHitPointsZeroOrLess()
        {
            return hitPoints <= 0;
        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
        }
    }
}