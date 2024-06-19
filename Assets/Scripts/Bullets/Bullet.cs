using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [field: NonSerialized] public Team Team { get; set; }
        [field: NonSerialized] public int Damage { get; set; }

        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void Shoot(BulletSpawner.BulletData bulletData)
        {
            var bulletConfig = bulletData.BulletConfig;

            Damage = bulletConfig.damage;
            Team = bulletConfig.team;
            gameObject.layer = (int)bulletConfig.physicsLayer;
            spriteRenderer.color = bulletConfig.color;

            transform.position = bulletData.Position;
            rigidbody2D.velocity = bulletData.Velocity;
        }
    }
}