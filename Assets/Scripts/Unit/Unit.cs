using System;
using Components;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Unit : MonoBehaviour, IDamageable
    {
        [SerializeField] private UnitConfig unitConfig;

        [SerializeField] private HitPointsComponent hitPointsComponent;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private TeamComponent teamComponent;
        [SerializeField] private WeaponComponent weaponComponent;

        public Action<Unit> OnDeath;

        [Inject]
        public void Construct(BulletSpawner bulletSpawner)
        {
            hitPointsComponent.Construct(unitConfig.initialHealth);
            moveComponent.Construct(unitConfig.speed);
            teamComponent.Construct(unitConfig.team);
            weaponComponent.Construct(bulletSpawner);
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