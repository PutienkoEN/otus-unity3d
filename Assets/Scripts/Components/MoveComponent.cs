using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class MoveComponent
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 5.0f;

        public void MoveTo(Vector2 vector)
        {
            var nextPosition = rigidbody2D.position + vector * speed;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}