using System;
using Components;
using UnityEngine;

namespace ShootEmUp
{
    public class Unit : MonoBehaviour, IDamageable
    {
        [SerializeField] private HitPointsComponent hitPointsComponent = new();
        [SerializeField] private MoveComponent moveComponent = new();
        [SerializeField] private TeamComponent teamComponent = new();
        [SerializeField] private WeaponComponent weaponComponent = new();

        public Action<Unit> OnDeath;

        private void Awake()
        {
            var findObjectOfType = FindObjectOfType<BulletSpawner>();
            weaponComponent.Initialize(findObjectOfType);
        }

        public void TakeDamage(int damage, Team team)
        {
            if (teamComponent.SameTeam(team))
            {
                return;
            }

            if (hitPointsComponent.IsHitPointsZeroOrLess())
            {
                return;
            }

            hitPointsComponent.TakeDamage(damage);

            // Since we took damage, we recheck hit points once again.
            if (hitPointsComponent.IsHitPointsZeroOrLess())
            {
                OnDeath?.Invoke(this);
            }
        }

        public void Attack(Transform target)
        {
            weaponComponent.Attack(target);
        }

        public void MoveTo(Vector2 direction)
        {
            moveComponent.MoveTo(direction);
        }
    }
}