using System;
using Components;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Unit : MonoBehaviour, IDamageable
    {
        [SerializeField] private HitPointsComponent hitPointsComponent;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private TeamComponent teamComponent;
        [SerializeField] private WeaponComponent weaponComponent = new();

        public Action<Unit> OnDeath;

        [Inject]
        public void Construct(
            HitPointsComponent hitPointsComponent,
            MoveComponent moveComponent,
            TeamComponent teamComponent)
        {
            this.hitPointsComponent = hitPointsComponent;
            this.moveComponent = moveComponent;
            this.teamComponent = teamComponent;
        }

        private void Awake()
        {
            var findObjectOfType = FindObjectOfType<BulletFactory>();
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