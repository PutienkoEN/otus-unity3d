using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyMoveAgent : MonoBehaviour, IGamePauseListener
    {
        private Transform destination;
        private Transform currentPosition;

        private bool isReached;
        private float targetReachedMagnitude;

        public Action OnTargetReached;
        public Action<Vector2> OnMove;

        public void Initialize(Transform currentPosition, Transform destination, float targetReachedMagnitude)
        {
            isReached = false;
            this.targetReachedMagnitude = targetReachedMagnitude;
            this.currentPosition = currentPosition;
            this.destination = destination;
        }

        private void Awake()
        {
            IGameListener.Register(this);
        }

        private void FixedUpdate()
        {
            if (isReached)
            {
                OnTargetReached?.Invoke();
                return;
            }

            var vector = (Vector2)destination.position - (Vector2)currentPosition.position;
            if (vector.magnitude <= targetReachedMagnitude)
            {
                isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            OnMove?.Invoke(direction);
        }

        public void OnGamePause()
        {
            enabled = false;
        }
    }
}