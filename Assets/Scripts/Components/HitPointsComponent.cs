using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public class HitPointsComponent
    {
        [SerializeField] private int hitPoints;
        
        [Inject]
        public HitPointsComponent(int hitPoints)
        {
            this.hitPoints = hitPoints;
        }

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