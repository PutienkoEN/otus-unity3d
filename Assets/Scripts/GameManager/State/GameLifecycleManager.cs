using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameLifecycleManager : MonoBehaviour
    {
        [SerializeField] private GameStateStorage gameStateStorage;

        private readonly List<IGameUpdateListener> gameUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> fixedUpdateListeners = new();

        public void Awake()
        {
            IGameListener.OnRegister += AddListener;
        }

        private void AddListener(IGameListener gameListener)
        {
            if (gameListener is IGameUpdateListener gameUpdateListener)
            {
                gameUpdateListeners.Add(gameUpdateListener);
            }

            if (gameListener is IGameFixedUpdateListener gameFixedUpdateListener)
            {
                fixedUpdateListeners.Add(gameFixedUpdateListener);
            }
        }

        private void Update()
        {
            if (gameStateStorage.GetCurrentState() != GameState.InProgress)
            {
                return;
            }

            TriggerGameUpdateListeners();
        }

        private void TriggerGameUpdateListeners()
        {
            var deltaTime = Time.deltaTime;
            gameUpdateListeners.ForEach(listener => listener.OnUpdate(deltaTime));
        }

        private void FixedUpdate()
        {
            if (gameStateStorage.GetCurrentState() != GameState.InProgress)
            {
                return;
            }

            TriggerGameFixedUpdateListeners();
        }

        private void TriggerGameFixedUpdateListeners()
        {
            var deltaTime = Time.fixedDeltaTime;
            fixedUpdateListeners.ForEach(listener => listener.OnFixedUpdate(deltaTime));
        }
    }
}