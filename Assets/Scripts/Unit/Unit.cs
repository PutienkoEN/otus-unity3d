using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Unit : MonoBehaviour
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

        public void TakeDamage(int damage)
        {
            hitPointsComponent.TakeDamage(damage);
            if (!hitPointsComponent.IsHitPointsExists())
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

        public bool IsAlive()
        {
            return hitPointsComponent.IsHitPointsExists();
        }

        public Team GetTeam()
        {
            return teamComponent.Team;
        }
    }
}