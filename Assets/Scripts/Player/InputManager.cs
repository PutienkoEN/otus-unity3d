using System;
using UnityEngine;

namespace ShootEmUp
{
    public class InputManager : MonoBehaviour,
        IGameUpdateListener,
        IGameFixedUpdateListener
    {
        public Action<Vector2> OnMoveInput;
        public Action OnShootInput;

        private void Awake()
        {
            IGameListener.Register(this);
            enabled = false;
        }

        public void OnUpdate(float deltaTime)
        {
            HandleShootInput();
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            HandleMoveInput(fixedDeltaTime);
        }

        private void HandleShootInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootInput?.Invoke();
            }
        }

        private void HandleMoveInput(float fixedDeltaTime)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveToDirection(Vector2.left, fixedDeltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveToDirection(Vector2.right, fixedDeltaTime);
            }
        }

        private void MoveToDirection(Vector2 direction, float fixedDeltaTime)
        {
            var directionWithDeltaTime = direction * fixedDeltaTime;
            OnMoveInput?.Invoke(directionWithDeltaTime);
        }
    }
}