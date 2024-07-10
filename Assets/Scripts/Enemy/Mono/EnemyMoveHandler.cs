using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyMoveHandler : MonoBehaviour
    {
        public Action<Unit> TargetReached;
    
        private readonly List<MoveCommand> moveCommands = new();
        private Settings settings;

        [Inject]
        public void Construct(Settings settings)
        {
            this.settings = settings;
        }
            
        public void Move(Unit unit, Transform target)
        {
            moveCommands.Add(new MoveCommand(unit, target));
        }
        
        // Could be optimized to clean up stale commands
        public void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;

            moveCommands
                .Where(moveCommand => moveCommand.CanMove)
                .ToList()
                .ForEach(moveCommand => Move(moveCommand, fixedDeltaTime));
        }

        private void Move(MoveCommand moveCommand, float fixedDeltaTime)
        {
            var vector = (Vector2)moveCommand.Target.position - (Vector2)moveCommand.CurrentPosition.position;
            if (vector.magnitude <= settings.targetReachedMagnitude)
            {
                moveCommand.CanMove = false;
                TargetReached?.Invoke(moveCommand.Unit);
            }

            var direction = vector.normalized * fixedDeltaTime;
            moveCommand.Unit.MoveTo(direction);
        }

        private class MoveCommand
        {
            public readonly Unit Unit;
            public readonly Transform CurrentPosition;
            public readonly Transform Target;
            public bool CanMove = true;

            public MoveCommand(Unit unit, Transform target)
            {
                Unit = unit;
                CurrentPosition = unit.transform; // cache
                Target = target;
            }
        }

        [Serializable]
        public class Settings
        {
            public float targetReachedMagnitude;
        }
    }
}