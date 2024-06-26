using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyMoveAgent : MonoBehaviour
    {
        private Transform destination;
        private Transform currentPosition;

        private bool isReached;

        public Action OnTargetReached;
        public Action<Vector2> OnMove;

        public void Initialize(Transform currentPosition, Transform destination)
        {
            isReached = false;
            this.currentPosition = currentPosition;
            this.destination = destination;
        }

        private void FixedUpdate()
        {
            if (isReached)
            {
                OnTargetReached?.Invoke();
                return;
            }

            var vector = (Vector2)destination.position - (Vector2)currentPosition.position;
            if (vector.magnitude <= 0.25f)
            {
                isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            OnMove?.Invoke(direction);
        }
    }
}