using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyAttackAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float countdown;

        private Unit target;
        private Unit attacker;

        private float currentTime;
        private bool canAttack;

        public Action<Transform> OnAttack;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (!canAttack)
            {
                return;
            }

            currentTime -= fixedDeltaTime;
            if (!(currentTime <= 0))
            {
                return;
            }

            OnAttack?.Invoke(target.transform);
            currentTime += countdown;
        }

        public void Initialize(Unit attacker, Unit target)
        {
            this.attacker = attacker;
            this.target = target;

            this.attacker.OnDeath += DisableAttack;
        }

        public void EnableAttack()
        {
            canAttack = true;
        }

        private void DisableAttack(Unit unit)
        {
            canAttack = false;
            attacker.OnDeath -= DisableAttack;
        }
    }
}