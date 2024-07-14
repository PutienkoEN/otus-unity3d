using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GameLifecycleManager : ITickable, IFixedTickable
    {
        private readonly GameStateStorage gameStateStorage;

        private readonly List<IGameUpdateListener> gameUpdateListeners;
        private readonly List<IGameFixedUpdateListener> fixedUpdateListeners;

        [Inject]
        public GameLifecycleManager(
            GameStateStorage gameStateStorage,
            List<IGameUpdateListener> gameUpdateListeners,
            List<IGameFixedUpdateListener> fixedUpdateListeners)
        {
            this.gameStateStorage = gameStateStorage;
            this.gameUpdateListeners = gameUpdateListeners;
            this.fixedUpdateListeners = fixedUpdateListeners;
        }

        public void Tick()
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

        public void FixedTick()
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


        //
        // public void Awake()
        // {
        //     IGameListener.OnRegister += AddListener;
        // }
        //
        // public void OnDestroy()
        // {
        //     IGameListener.OnRegister -= AddListener;
        // }
        //
        // private void AddListener(IGameListener gameListener)
        // {
        //     if (gameListener is IGameUpdateListener gameUpdateListener)
        //     {
        //         gameUpdateListeners.Add(gameUpdateListener);
        //     }
        //
        //     if (gameListener is IGameFixedUpdateListener gameFixedUpdateListener)
        //     {
        //         fixedUpdateListeners.Add(gameFixedUpdateListener);
        //     }
        // }
    }
}