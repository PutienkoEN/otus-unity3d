using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public class PlayerAttackAgent
    {
        [SerializeField] private Unit unit;
        [SerializeField] private GameObject target;

        public void Attack()
        {
            unit.Attack(target.transform);
        }
    }
}