using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [Serializable]
    public class MoveComponent
    {
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rigidbody2D;

        public void Construct(float speed)
        {
            this.speed = speed;
        }

        public void MoveTo(Vector2 vector)
        {
            var nextPosition = rigidbody2D.position + vector * speed;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}