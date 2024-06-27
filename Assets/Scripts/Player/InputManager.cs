using System;
using UnityEngine;

namespace ShootEmUp
{
    public class InputManager : MonoBehaviour, IGamePauseListener
    {
        public Action<Vector2> OnMoveInput;
        public Action OnShootInput;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        private void Update()
        {
            HandleShootInput();
            HandleMoveInput();
        }

        private void HandleShootInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootInput?.Invoke();
            }
        }

        private void HandleMoveInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveToDirection(Vector2.left);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveToDirection(Vector2.right);
            }
        }

        private void MoveToDirection(Vector2 direction)
        {
            var directionWithDeltaTime = direction * Time.fixedDeltaTime;
            OnMoveInput?.Invoke(directionWithDeltaTime);
        }

        public void OnGamePause()
        {
            enabled = false;
        }
    }
}