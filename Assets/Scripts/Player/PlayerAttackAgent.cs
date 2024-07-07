using System;
using DI;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public class PlayerAttackAgent
    {
        private Unit unit;
        private GameObject target;

        public PlayerAttackAgent(
            [Inject(Id = SceneInstaller.Character)]
            Unit unit,
            [Inject(Id = SceneInstaller.CharacterTarget)]
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