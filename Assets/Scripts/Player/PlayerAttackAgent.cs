using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public class PlayerAttackAgent
    {
        private Unit character;
        private GameObject target;

        [Inject]
        public PlayerAttackAgent(LevelProvider levelProvider, GameObject target)
        {
            character = levelProvider.characterObject;
            this.target = target;
        }

        public void Attack()
        {
            character.Attack(target.transform);
        }
    }
}