using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public class MoveComponent
    {
        [SerializeField] private float speed;

        private Rigidbody2D rigidbody2D;

        [Inject]
        public MoveComponent(float speed, Rigidbody2D rigidbody2D)
        {
            this.speed = speed;
            this.rigidbody2D = rigidbody2D;
        }

        public void MoveTo(Vector2 vector)
        {
            var nextPosition = rigidbody2D.position + vector * speed;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}