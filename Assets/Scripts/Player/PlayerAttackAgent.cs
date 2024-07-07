using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class PlayerAttackAgent
    {
        private Unit unit;
        private GameObject target;

        public PlayerAttackAgent(
            Unit unit,
            GameObject target)
        {
            this.unit = unit;
            this.target = target;
        }

        public void Attack()
        {
            unit.Attack(target.transform);
        }
    }
}